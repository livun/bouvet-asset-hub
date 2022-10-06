# Delete asset
## Delete wrongly added asset
```mermaid
sequenceDiagram
    User ->> UI : Toggle delet icon on asset from asset table
    Activate User
    
        Activate UI
            UI -->> User : Warning to confirm delete
            User ->> UI : Confirm delete
        Deactivate UI
    Deactivate User
    
    UI ->> API Controller: Post data
    API Controller -) Mediator Handler : Request to delete asset

    Alt is valid delete
        
        Mediator Handler -) Asset Repository : DeleteAsset(data)
        Asset Repository  --) Mediator Handler : Response(Asset)
        Mediator Handler -->> API Controller : Response('Asset successfully deleted')
        API Controller --) UI  : Asset sucessfully removed
        UI -->> User : Table is updated with information

    else is not valid to delete
        Mediator Handler -->> API Controller : Response('Asset cannot be deleted')
        API Controller --) UI  : Asset cannot be removed
        UI -->> User : Message with repsonse

    end