# Command Template: `/speckit.checklist`

Use this command to generate review checklists for a feature or release.

## Required Actions

1. Build checklist items from `spec.md`, `plan.md`, and `tasks.md`.
2. Include governance checks for:
   - branch/PR compliance
   - constitution gate compliance
   - test evidence completeness
   - coverage evidence (`>=70%` minimum, `>=80%` target tracking)
   - .NET 10 + C# 14.0 toolchain alignment
   - NuGet dependency currency / pinning documentation
   - documentation completeness (bilingual + XML + DocFX when required)

## Validation Checklist

- Checklist items are actionable and testable.
- Each item maps to a concrete artifact or command output.
