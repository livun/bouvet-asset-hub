# Read Asset
- [View one Asset from scan](#view-one-asset-from-scan)
- [View one Asset from table](#view-one-asset-from-table)
- [View all Assets](#view-all-assets)
- [View Assets by Category](#view-assets-by-category)
## View one Asset from scan
```mermaid
sequenceDiagram
User ->> UI : Scan Asset
    Activate UI
        UI ->>+ API /assets/{id}: GET Asset by Id
        API /assets/{id} ->>+ GetAssetByIdQueryHandler : GetAssetByIdQuery (Id)
        GetAssetByIdQueryHandler ->>+ Asset Repository : Get(Id)
        Asset Repository -->>- GetAssetByIdQueryHandler : Response (Asset)
        GetAssetByIdQueryHandler -->>- API /assets/{id} : Response (Asset)
        API /assets/{id} -->>- UI : Response (Asset)
        UI -->> User: Display Asset
    Deactivate UI

```
## View one Asset from table
```mermaid
sequenceDiagram
User ->> UI : Select one Asset
   Activate UI
        UI ->>+ API /assets/{id}: GET Asset By Id
        API /assets/{id} ->>+ GetAssetbyIdQueryHandler : GetAssetByIdQuery (Id)
        GetAssetbyIdQueryHandler ->>+ Asset Repository : Get (Id)
        Asset Repository -->>- GetAssetbyIdQueryHandler : Response (Asset)
        GetAssetbyIdQueryHandler -->>- API /assets/{id} : Response (Asset)
        API /assets/{id} -->>- UI : Response (Asset)
        UI -->> User: Display Asset
    Deactivate UI

```
## View all Assets
```mermaid
sequenceDiagram
User ->> UI : View table of Assets
    Activate UI
        UI ->>+ API /assets: GET Assets
        API /assets ->>+ GetAssetsQueryHandler : GetAssetsQuery
        GetAssetsQueryHandler ->>+ Asset Repository : GetAll()
        Asset Repository -->>- GetAssetsQueryHandler : Response(List<Asset>)
        GetAssetsQueryHandler -->>- API /assets : Response (List<Asset>)
        API /assets -->>- UI : Response (List<Asset>)
        UI -->> User: Display Asset
    Deactivate UI

```

## View Assets by Category
```mermaid
sequenceDiagram
User ->> UI : Toggle Category in Assets table
    Activate UI
        UI ->>+ API /categories/{id}/assets: GET Assets by Category 
        API /categories/{id}/assets ->>+ GetAssetsByCategoryQueryHandler : GetAssetsByCategoryQuery (DTO)
        GetAssetsByCategoryQueryHandler ->>+ Asset Repository : GetByCategory(Data)
        Asset Repository -->>- GetAssetsByCategoryQueryHandler : Response(List<Asset>)
        GetAssetsByCategoryQueryHandler -->>- API /categories/{id}/assets : Response (List<Asset>)
        API /categories/{id}/assets -->>- UI : Response (List<Asset>)
        UI -->> User: Display Assets Table
    Deactivate UI

```