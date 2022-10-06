# Update asset
## Update asset information
```mermaid
sequenceDiagram
    User ->> UI : Toggle update icon on asset from asset table
    
    Activate UI
        UI -->>+ User : Form with input fields
        User ->>- UI : Filled out form
    Deactivate UI
    
    UI ->>+ API Controller: Post data
    API Controller -)+ Mediator Handler : Request to update asset
        
    Mediator Handler -)+ Asset Repository : UpdateAsset(data)
            
    Asset Repository  --)- Mediator Handler : Response(Asset)
                    
    Mediator Handler -->>- API Controller : Response('Asset successfully updated')
    
    API Controller --)- UI  : Asset sucessfully updated
    
    UI -->> User : Table is updated with information
```
## Update asset status on one or more assets
```mermaid
sequenceDiagram
    User ->> UI : Toggle all assets that should be updated
    
    Activate User
        User ->> UI : Toggle update status button
        
        Activate UI
            UI -->> User : Form with dropdown field
            User ->> UI : Filled out form with chosen status
        Deactivate UI
    
    Deactivate User
    
    UI ->>+ API Controller: Post data (list of assets)
    
    API Controller -)+ Mediator Handler : Request to update asset status

    alt length of list is one
        Mediator Handler -)+ Asset Repository : UpdateAssetStatus(data) 
        Asset Repository  --)- Mediator Handler : Response(Asset) 
    else length of list is > one
        Mediator Handler -)+ Asset Repository : UpdateAssetsStatuses(data) 
        Asset Repository  --)- Mediator Handler : Response(Asset) 
    end
        
    Mediator Handler -->>- API Controller : Response('Asset successfully updated')
            
    API Controller --)- UI  : Asset sucessfully updated
    
    UI -->> User : Table is updated with information
     
```
## Update asset information and status from scan
### Used sequences in this diagram
- [View one asset from scan](read-asset.md#view-one-asset-from-scan)
```mermaid
    sequenceDiagram
        Note over User : View one asset from scan
        Note over User: Actions on Asset
            
            Alt Update Asset
            
                UI -->> User : Form with input fields
                Activate UI
                Activate User
                User ->> UI : Filled out form 

            End
        Deactivate User
        
        UI ->>+ API Controller: Post data
        
        API Controller -)+ Mediator Handler : Request to update asset
            
        Mediator Handler -)+ Asset Repository : UpdateAsset(data)
                
        Asset Repository  --)- Mediator Handler : Response(Asset)
                        
        Mediator Handler -->>- API Controller : Response('Asset successfully updated')
        
        API Controller --)- UI  : Asset sucessfully updated
        
        UI -->> User : Table is updated with information
        Deactivate UI

     
```