// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Runtime.Versioning;
using System.Security.AccessControl;
using System.Security.Principal;

namespace TuiVision.Core;

/// <summary>
/// Besitzt eine private Ausgabewurzel und löst linkfreie relative Ziele auf.
///
/// Owns a private output root and resolves link-free relative targets.
/// </summary>
public sealed class TControlledOutputDirectory : IDisposable
{
    private TControlledOutputDirectory(string rootPath) => RootPath = rootPath;

    /// <summary>Private Ausgabewurzel. / Private output root.</summary>
    public string RootPath { get; }

    /// <summary>Zeigt die Freigabe an. / Indicates disposal.</summary>
    public bool IsDisposed { get; private set; }

    /// <summary>
    /// Erstellt eine private Temp-Wurzel. Prozesse derselben OS-Identität bilden
    /// eine Vertrauensdomäne; der Pfad darf nicht geringer vertrauten Komponenten
    /// offengelegt werden.
    ///
    /// Creates a private temporary root. Processes with the same OS identity form
    /// one trust domain; the path must not be disclosed to lower-trust components.
    /// </summary>
    /// <returns>Besitzende Output-Capability. / Owning output capability.</returns>
    public static TControlledOutputDirectory Create()
    {
        string root = Directory.CreateTempSubdirectory("tuivision-output-").FullName;
        try
        {
            Protect(root);
            return new TControlledOutputDirectory(root);
        }
        catch
        {
            if (Directory.Exists(root) && !IsReparsePoint(root))
            {
                Directory.Delete(root, recursive: true);
            }

            throw;
        }
    }

    /// <summary>
    /// Löst ein neues relatives Ziel auf und lehnt Links sowie Traversal ab.
    ///
    /// Resolves a new relative target and rejects links and traversal.
    /// </summary>
    /// <param name="relativePath">Relativer Zielpfad. / Relative target path.</param>
    /// <returns>Vollständiger Zielpfad. / Full target path.</returns>
    /// <exception cref="ArgumentException">Der Pfad ist leer. / The path is empty.</exception>
    /// <exception cref="InvalidOperationException">Der Pfad verlässt die Wurzel oder nutzt einen Link. / The path leaves the root or uses a link.</exception>
    public string ResolveNewFile(string relativePath)
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);
        ArgumentException.ThrowIfNullOrWhiteSpace(relativePath);
        if (Path.IsPathRooted(relativePath))
        {
            throw new InvalidOperationException("Only relative output paths are accepted.");
        }

        string candidate = Path.GetFullPath(Path.Combine(RootPath, relativePath));
        string prefix = RootPath + Path.DirectorySeparatorChar;
        StringComparison comparison = OperatingSystem.IsWindows()
            ? StringComparison.OrdinalIgnoreCase
            : StringComparison.Ordinal;
        if (!candidate.StartsWith(prefix, comparison))
        {
            throw new InvalidOperationException("Output path leaves the controlled root.");
        }

        string current = RootPath;
        foreach (string segment in Path.GetRelativePath(RootPath, candidate).Split(
                     [Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar],
                     StringSplitOptions.RemoveEmptyEntries))
        {
            current = Path.Combine(current, segment);
            if (IsReparsePoint(current))
            {
                throw new InvalidOperationException("Output paths must not cross symbolic links or reparse points.");
            }
        }

        return candidate;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (IsDisposed)
        {
            return;
        }

        IsDisposed = true;
        if (Directory.Exists(RootPath) && !IsReparsePoint(RootPath))
        {
            Directory.Delete(RootPath, recursive: true);
        }
    }

    private static void Protect(string root)
    {
        if (!OperatingSystem.IsWindows())
        {
            File.SetUnixFileMode(root, UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute);
            return;
        }

        ProtectWindows(root);
    }

    [SupportedOSPlatform("windows")]
    private static void ProtectWindows(string root)
    {
        using WindowsIdentity identity = WindowsIdentity.GetCurrent();
        SecurityIdentifier owner = identity.User
            ?? throw new InvalidOperationException("The current Windows identity has no security identifier.");
        SecurityIdentifier system = new(WellKnownSidType.LocalSystemSid, null);
        SecurityIdentifier administrators = new(WellKnownSidType.BuiltinAdministratorsSid, null);
        DirectorySecurity security = new();
        security.SetOwner(owner);
        security.SetAccessRuleProtection(isProtected: true, preserveInheritance: false);
        InheritanceFlags inheritance = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;
        foreach (SecurityIdentifier trusted in new[] { owner, system, administrators })
        {
            security.AddAccessRule(new FileSystemAccessRule(
                trusted,
                FileSystemRights.FullControl,
                inheritance,
                PropagationFlags.None,
                AccessControlType.Allow));
        }

        DirectoryInfo directory = new(root);
        FileSystemAclExtensions.SetAccessControl(directory, security);
        DirectorySecurity applied = FileSystemAclExtensions.GetAccessControl(directory);
        HashSet<string> trustedValues = [owner.Value, system.Value, administrators.Value];
        if (!applied.AreAccessRulesProtected
            || applied.GetAccessRules(true, true, typeof(SecurityIdentifier))
                .Cast<FileSystemAccessRule>()
                .Any(rule => rule.AccessControlType == AccessControlType.Allow
                    && !trustedValues.Contains(((SecurityIdentifier)rule.IdentityReference).Value)))
        {
            throw new InvalidOperationException("The private Windows output ACL is not confined.");
        }
    }

    private static bool IsReparsePoint(string path)
    {
        // Exists folgt einem Link und ist bei einem verwaisten Link false. Die
        // LinkTarget-Metadaten prüfen deshalb zuerst den Verzeichniseintrag selbst.
        // Exists follows links and is false for dangling links, so LinkTarget
        // metadata checks the directory entry itself first.
        if (new FileInfo(path).LinkTarget is not null || new DirectoryInfo(path).LinkTarget is not null)
        {
            return true;
        }

        try
        {
            return File.GetAttributes(path).HasFlag(FileAttributes.ReparsePoint);
        }
        catch (FileNotFoundException)
        {
            return false;
        }
        catch (DirectoryNotFoundException)
        {
            return false;
        }
    }
}
