# 0005: Co-locate DTO records with their entity; ApplicationCore owns entity contracts

## Status
Accepted

## Context
Kalmia's entities live in `Kalmia.ApplicationCore`, and DTOs representing the data contract for a given entity (e.g., `ActivityDto`, `CreateActivityDto`) also live in ApplicationCore rather than the API layer, per the earlier decision that "what's safe to expose" is a business-layer concern, not a presentation-layer one.

Given that placement, a further question is file organization: whether an entity's DTOs should live in their own dedicated files, or in the same file as the entity itself. Kalmia's established file-organization convention groups everything related to a given entity together (entity-named files, all CRUD grouped per entity), and an entity's DTOs are tightly related to it — they exist specifically to represent that entity's data in different shapes (read, create), and typically change in lockstep with the entity's own properties.

eShopOnWeb's `CatalogItemDetails` is a related but narrower case — a nested record used only internally by the entity's own `UpdateDetails()` method, not a general-purpose API contract. Kalmia's DTOs are top-level, non-nested record types, used directly by the API layer to shape request/response bodies.

It is possible that Kalmia will later need a second layer of DTOs owned by the API layer itself, distinct from ApplicationCore's DTOs — similar to eShopOnWeb's `PublicApi` project, which defines its own request/response models separate from `ApplicationCore`. This would apply if a UI-specific shape diverges meaningfully from the shape ApplicationCore considers canonical (e.g., a combined/aggregated view assembled from multiple entities, or a UI-only computed field). This ADR does not resolve that question; it addresses only where ApplicationCore's own entity-level DTOs are organized within ApplicationCore itself.

## Decision
DTO records for a given entity are declared as separate top-level types (not nested inside the entity class) in the same file as the entity, e.g., `Activity.cs` contains `class Activity`, `record ActivityDto`, `record CreateActivityDto`, and the static mapping extension methods between them.

ApplicationCore continues to own and define these DTOs, dictating what shape of its entities is safe and appropriate to expose, independent of any particular consumer (Angular today, potentially React or another consumer later).

If a future need arises for API-layer-specific request/response models that diverge from ApplicationCore's entity DTOs, those will be introduced as a separate, explicitly-scoped decision (a new or amended ADR) rather than assumed now.

## Consequences
+ One file per entity remains the complete, self-contained reference for that entity — model, its DTO contracts, and the mapping between them — consistent with the project's established file-organization convention.
+ DTO shape and entity shape stay visibly connected during development, since editing one is likely to prompt noticing the other in the same file.
+ ApplicationCore retains control over what's exposed, without requiring a second DTO-mapping layer in the API project for the common case.
- Entity files grow as an entity accumulates more DTOs (e.g., list view, detail view, create, update) — acceptable at Kalmia's current entity count and complexity; may need reconsideration if any single entity's file becomes unwieldy.
- Leaves open, deliberately, the question of whether a second API-layer DTO tier will eventually be needed; this decision covers only ApplicationCore's internal organization, not a commitment against ever introducing that additional layer.