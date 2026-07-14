@{
    Severity = @('Error', 'Warning')
    IncludeDefaultRules = $true

    # These rules conflict with established CLI output, UTF-8-no-BOM policy,
    # stable public function names, or repository-owned preview semantics.
    ExcludeRules = @(
        'PSAvoidUsingWriteHost'
        'PSReviewUnusedParameter'
        'PSShouldProcess'
        'PSUseApprovedVerbs'
        'PSUseBOMForUnicodeEncodedFile'
        'PSUseDeclaredVarsMoreThanAssignments'
        'PSUseShouldProcessForStateChangingFunctions'
        'PSUseSingularNouns'
    )
}
