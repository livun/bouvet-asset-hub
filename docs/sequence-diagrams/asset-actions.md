## Actions on Asset
When scanning an Asset, the Status of it decides the possible Actions available
### Used sequences in this diagram
- [Sequence: View one Asset from scan](read-Asset.md#view-one-Asset-from-scan)
- [Sequence: Update Asset](update-Asset.md#update-Asset-information-and-status-from-scan)
- [Sequence: New Loan](loan.md#new-loan)
- [Sequence: Hand in Loan](loan.md#hand-in-loan)
```mermaid
sequenceDiagram
   
        Note over User : Sequence: View one Asset from scan
      

        Note over User: Actions on Asset

        alt Asset Status is Registered
            User ->> UI: Update Status
                    
            Note over User, UI: Sequence: Update Asset
        Else Asset Status is Available
            User ->> UI: New Loan or Update Status
                alt Update Status
                    Note over User, UI : Sequence: Update Asset 
                else New Loan
                    Note over User, UI : Sequence: New Loan
                end
            
        else Asset Status is Unvailable
            Note over User, UI : Sequence: Hand in Loan

        Else Asset Status is Discontinued
            User ->> UI: Update Status
            Note over User, UI: Sequence: Update Asset

        End
    
    
```