# 0003: Controllers as primary Web API style over Minimal APIs

## Status
Accepted

## Context
Asp.Net Core supports two primary styles of building HTTP APIs: MVC-style controllers and Minimial APIs. While Microsoft and the overall ecosystem are pushing minimal apis lately, controllers remain the dominant design in large enterprise level code bases, which is the target reference style for this portfolio project.

Minimal api's hold appeal, and validation is now on par with controllers which was the major drawback, but it remains to be seen how widely adopted they will be at large enterprises. Consideration for including a sample minimal api for some function at a later time.

## Decision
Use MVC-style controllers as the default approach for Kalmia's web api endpoint layer. Controllers expose REST-style endpoints.

## Consequences
+ Demonstrates the pattern most reviewers and existing job postings in the target market will recognize and expect.
+ Framework-provided ceremony is inherited from 'ControllerBase' rather than hand-built.
- More boilerplate per endpoint compared to minimal api's.
- Primary implementation doesn't showcase the more current minimal api style.