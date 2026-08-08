# 0002: Dependency Inversion, applied pragmatically (not dogmatic Clean Architecture)

## Status
Accepted

## Context
Kalmia is a solo portfolio project to demonstrate solid understanding and implementation of an enterprise level asp.net core web api with database access and various clients. Research was conducted on the latest design principles and conventions starting with the Microsoft Learn information, tutorials, and architecture guidance on asp.net core and entity framework core.

The main architecture reference comes from the msft guidance "Architecting Modern Web Applications with ASP.NET Core and Azure" by Steve Smith (ardalis) and the associated eShopOnWeb reference project. Other sources were also reviewed during research and ramp up (Julio Casal and Learn Smart Coding on youtube and their asp.net core web api tutorials while a few others reviewed were discarded as not valuable) as well as discussions with Claude chat. Items reviewed included SOLID principles, Dependency Inversion Principle (DIP), Clean Architecture, Vertical Slice Architecture, and comparing against experience with traditional N-Tier design.

## Decision
Strict adherence to dependency inversion principle while loosely following clean architecture and the eShopOnWeb reference.

- Kalmia.Core: holds domain entities and associated business logic, no other dependencies
- Kalmia.Infrastructure: holds data and identity logic, with EF Core DbContext and repository implementations, references Core. 
- Kalmia.Api: asp.net core web api using dependency injection, references Core and Infastructure.
- presentation layer clients will be created later

## Consequences
+ Business logic and domain entities remain testable in isolation, with no database or web framework required to run unit tests against them.
- Strict inward-only references require discipline to maintain as project grows