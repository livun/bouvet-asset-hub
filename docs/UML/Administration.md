# Tag new asset
```mermaid
sequenceDiagram
    User->>UI /addAsset: Choose "new asset" from menu
    
    loop 
        UI->>UI: Do OCR and identify serialnumber
    end
    UI ->> External API: Validate serial number
    activate External API
    External API --) UI: response
    UI ->> API Controller: Send validatet serial number
    
```