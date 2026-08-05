# 0001: Minimize architectural ceremony; hand-roll on top of the BCL wherever practical

## Status
Accepted

## Context
Kalmia is a solo portfolio project intended to demonstrate senior-level .NET engineering judgment, with a particular emphasis on depth in C# and close work with the database layer. Common "Clean Architecture" tutorials and reference projects (including eShopOnWeb, used as Kalmia's primary architectural reference) typically pair the core dependency-inversion structure with a broader set of supporting libraries and patterns — MediatR for CQRS, AutoMapper for object mapping, a generic Repository and Specification pattern, a shared `BaseEntity` abstraction, and similar.

These additions are individually reasonable in the contexts they were designed for (larger teams, multiple consumers, need for consistency across many contributors), but each comes with a cost: added indirection, reflection-based runtime behavior in some cases, and a layer of "someone else's abstraction" between the developer and the actual problem being solved. For a solo developer building a portfolio project specifically to demonstrate hands-on skill, that cost is not obviously justified by the benefit, and adopting these patterns by default (because a reference project uses them) risks producing a codebase that showcases familiarity with libraries rather than demonstrated understanding of the underlying problems those libraries solve.

Two competing priorities are in tension here:
- Demonstrating deep, hands-on C# and database fluency, and avoiding ceremony that doesn't pay for itself at this project's actual scale (single developer, single consumer, moderate entity count).
- Demonstrating market-relevant fluency with tools and patterns that appear on real job postings (e.g., MediatR shows up frequently in senior .NET listings), and making efficient use of limited project time by not hand-building solved problems.

## Decision
Default to hand-written, explicit code over third-party libraries and generic/reusable abstractions, for both application logic and supporting infrastructure, unless a specific and articulable reason justifies an exception.

This applies at two levels:

1. **Third-party libraries**: avoided by default (MediatR, AutoMapper, FluentValidation, generic Repository/Specification packages). Object mapping, validation, and command/query dispatch are hand-written. Named exceptions, made deliberately rather than by default:
    - **EF Core** — the ORM itself; reimplementing this would not demonstrate additional skill relevant to the project's goals.
    - **ASP.NET Core Identity / auth libraries** — security-sensitive code (password hashing, token handling, session management) should use vetted, maintained libraries rather than hand-rolled implementations.
    - **Serilog** — structured logging is a solved problem with enough depth (sinks, enrichers, correlation) that reimplementing it doesn't add meaningful learning value.

2. **Micro-patterns / speculative abstractions**: avoided when they exist primarily to support generality Kalmia doesn't need — e.g., a shared `BaseEntity` class (supports a generic-repository pattern this project doesn't use), or an interface created for every entity regardless of whether more than one implementation will ever exist.

The .NET Base Class Library (BCL) is explicitly in scope and not considered a "third-party dependency" for purposes of this decision — `System.Xml.Linq`, `System.Text.Json`, `HttpClient`, LINQ, and similar are used freely, since fluency with the platform itself is part of the goal.

Dependency inversion and layered separation (ApplicationCore, DataAccess, Infrastructure, Api) are retained in full — this decision is about which libraries and micro-patterns fill those layers, not about abandoning the layering itself (see ADR 0002, dependency inversion).

## Consequences
+ Code in the repository more directly reflects the developer's own reasoning and implementation choices, which is the primary goal of a portfolio project.
+ Avoids reflection-heavy runtime behavior (e.g., AutoMapper's mapping resolution) in favor of explicit, debuggable, typically faster hand-written equivalents.
+ Fewer moving parts and dependencies to configure, version, and explain.
+ Each subsequent architectural decision in this project can reference this ADR as its underlying rationale, rather than re-justifying the same reasoning repeatedly.
- Reduced hands-on exposure to specific libraries (MediatR in particular) that appear frequently in job postings for target roles — mitigated by being able to explain the underlying pattern (e.g., mediator-style request/handler separation) and implement it by hand if asked.
- Hand-writing functionality that libraries already solve takes more development time than adopting an existing package — an explicit and accepted trade against this project's limited available time, made because the primary goal is demonstrated understanding, not delivery speed.
- Requires ongoing discipline to evaluate each new dependency decision individually rather than defaulting to whatever a reference project (eShopOnWeb) happens to use.