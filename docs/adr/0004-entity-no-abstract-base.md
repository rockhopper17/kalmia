# 0004: No base abstract class for entities

## Status
Accepted

## Context
Considered a shared BaseEntity abstract class (per eShopOnWeb) but rejected because it primarily supports a generic-repository pattern this project doesn't use, and each entity declaring its own Id keeps entities self-contained and explicit.

## Decision
Id field per entity class.

## Consequences
+ Less abstraction to keep track of
- More repeated code per entity class albeit minimal