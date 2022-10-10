# Tag new Assets
- [Tag Asset with Serial Number](#tag-asset-with-serial-number)
- [Tag Asset without Serial Number](#tag-asset-without-serial-number)
## Tag Asset with Serial Number
```mermaid
sequenceDiagram
    Note over User: Action: New Asset
    User ->> UI : Scan Serial Number
    Activate UI
        loop 
            UI ->> UI : Do OCR and identify Serial Number
        end
            UI  ->>+ External API: Validate Serial Number
   
                External API --)- UI : Response (Validation, MetaDTO)
                UI ->>+ API /assets: POST Asset (SerialNumber, MetaDTO, Category)
    Deactivate UI
                    API /assets -)+ CreateAssetCommandHandler : CreateAssetCommand (DTO)
                        Note over CreateAssetCommandHandler, Asset Repository : Check if Asset already exist
                        CreateAssetCommandHandler -)+ Asset Repository : GetBySerialNumber (SerialNumber) 
                        Asset Repository  -->>- CreateAssetCommandHandler : Response (Asset)
                    
            alt Asset is Some
                        
                    CreateAssetCommandHandler --) API /assets : Response('Asset aldready exist')
                API /assets --) UI  : Response ()
            UI -->> User : Message with response
                
            else Asset in None
                    
                        CreateAssetCommandHandler ->>+ QR Service : Generate QR code
                        QR Service -->>- CreateAssetCommandHandler : Response (QrCode)

                        CreateAssetCommandHandler -)+ Asset Repository : Add (Data)
                        Asset Repository  --)- CreateAssetCommandHandler : Response (Asset)

                    CreateAssetCommandHandler -->>- API /assets : Response ('Asset successfully added')
                API /assets --)- UI  : Response ()
                UI ->> External Print System : Request to print label
                External Print System -->> UI : Response ()
            UI -->> User : Message with repsonse
            Note over User, UI: Label is printed and stuck on asset
            end
        
```

## Tag asset without Serial Number
```mermaid
sequenceDiagram
    Note over User: Action: New Asset

  
    UI -->>+ User :  Form with dropdown field
    Activate UI
        User ->>- UI : Filled form with chosen Category
            UI ->>+ API /assets: POST Asset (Category)
                API /assets -)+ CreateAssetCommandHandler : CreateAssetCommand (DTO)
                    
                    CreateAssetCommandHandler ->>+ QR Service : Generate QR code
                    QR Service -->>- CreateAssetCommandHandler : Response (QR-code)
                    
                    CreateAssetCommandHandler -)+ Asset Repository : Add(Data)
                    Asset Repository  --)- CreateAssetCommandHandler : Response(Asset)
                    
                CreateAssetCommandHandler -->>- API /assets : Response('Asset successfully added')
            API /assets --)- UI  : Asset sucessfully added
            UI ->>+ External Print System : Request to print label
            External Print System -->>- UI : Response ()
        UI -->> User : Message with repsonse
    Deactivate UI
    Note over User, UI: Label is printed and stuck on asset
        

```