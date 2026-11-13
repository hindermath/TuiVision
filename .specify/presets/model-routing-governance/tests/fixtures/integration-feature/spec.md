# Specification: routing integration

Status: accepted synthetic test fixture, authorised only inside this temporary
repository. No real product feature, delivery, publication or installation.

As a routing operator I need exactly one package-free net10.0 console project
that prints MODEL_ROUTING_OK, so generated implementation profiles can be tested.

Acceptance: exactly one csproj, no PackageReference; build and run exit 0;
stdout exactly MODEL_ROUTING_OK with optional final LF. No application/package
network. Write verification.txt with MODEL_ROUTING_OK followed by LF and report
the normal phase-result JSON with its actual SHA-256. Provider access is allowed.
All implementation writes stay inside this repository. Expected task count: 1.
