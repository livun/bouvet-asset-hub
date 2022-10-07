# Update Asset
## Update Asset information
```mermaid
sequenceDiagram
    User ->> UI : Toggle update icon on Asset from Asset table
    Activate UI
    Activate User
        UI -->> User : Form with input fields
        User ->> UI : Filled out form
    Deactivate User
            UI ->>+ API /assets/{id}: PUT Asset By Id (data)
                API /assets/{id} -)+ UpdateAssetByIdCommandHandler : UpdateAssetByIdCommand (data)
                    UpdateAssetByIdCommandHandler -)+ Asset Repository : Update (data)
                    Asset Repository  --)- UpdateAssetByIdCommandHandler : Response()
                UpdateAssetByIdCommandHandler -->>- API /assets/{id} : Response ()
            API /assets/{id} --)- UI  : Response ()
        UI -->> User : If valid, table is updated with information
    Deactivate UI
```

## Update Asset Status on one or more Assets
```mermaid
sequenceDiagram
    User ->> UI : Toggle all Assets that should be updated
    Activate User
        User ->> UI : Toggle update status button
        Activate UI
            UI -->> User : Form with dropdown field
            User ->> UI : Filled out form with chosen status
    Deactivate User
                UI ->>+ API /assets/{assets}: PUT Asset By List of Id's (data)
                    API /assets/{assets} -)+ UpdateAssetsByIdsCommandHandler : UpdateAssetsByIdsCommand (data)
                    
                    alt length of list is one
                        UpdateAssetsByIdsCommandHandler -)+ Asset Repository : UpdateAssetStatus(id, data) 
                        Asset Repository  --)- UpdateAssetsByIdsCommandHandler : Response () 
                    else length of list is > one
                        UpdateAssetsByIdsCommandHandler -)+ Asset Repository : UpdateAssetsStatuses(List<id>, data) 
                        Asset Repository  --)- UpdateAssetsByIdsCommandHandler : Response () 
                    end
                    
                    UpdateAssetsByIdsCommandHandler -->>- API /assets/{assets} : Response ()
                API /assets/{assets} --)- UI  : Response ()
        Deactivate UI
            UI -->> User : If valid, table is updated with information
```
## Update Asset information and status from scan
### Used sequences in this diagram
- [View one Asset from scan](read-Asset.md#view-one-Asset-from-scan)
```mermaid
sequenceDiagram
    Note over User : View one Asset from scan
    Note over User: Action: Update Asset
    UI -->> User : Form with input fields
    Activate UI
    Activate User
        User ->> UI : Filled out form 
    Deactivate User
            UI ->>+ API /assets/{id}: PUT Asset By Id (data)
                API /assets/{id} -)+ UpdateAssetByIdCommandHandler : UpdateAssetByIdCommand (data)
                    UpdateAssetByIdCommandHandler -)+ Asset Repository : Update (id, data)
                    Asset Repository  --)- UpdateAssetByIdCommandHandler : Response ()
                UpdateAssetByIdCommandHandler -->>- API /assets/{id} : Response ()
            API /assets/{id} --)- UI  : Response ()
        UI -->> User : If valid, Asset is updated in view
    Deactivate UI     
```