## Actions on Asset
When scanning an Asset, the Status of it decides the possible Actions available
### Used sequences in this diagram
- [Sequence: View one Asset from scan](read-Asset.md#view-one-Asset-from-scan)
- [Sequence: Update Asset from scan](update-Asset.md#update-asset-information-and-status-from-scan)
- [Sequence: View one Loan from scan](read-loan.md#view-one-loan-from-scan)
- [Sequence: New Loan](loan-actions.md#new-loan)
- [Sequence: Extend Loan](loan-actions.md#extend-loan)
- [Sequence: Hand in Loan](loan-actions.md#hand-in-loan)

```mermaid
sequenceDiagram
   
        Note over User : Sequence: View one Asset from scan
      

        Note over User: Actions on Asset

        alt Asset Status is Registered
            User ->> UI: Update Status
                    
            Note over User, UI: Sequence: Update Asset from scan
        Else Asset Status is Available
            User ->> UI: Alternatives
                alt View Loan
                    Note over User, UI : Sequence: View one Loan from scan
                else Update Status
                    Note over User, UI : Sequence: Update Asset from scan
                else New Loan
                    Note over User, UI : Sequence: New Loan
                end
            
        else Asset Status is Unvailable
            User ->> UI : Alternatives
            alt Estend Loand
                Note over User, UI : Sequence: Extend Loan
            else Hand in Loan
                Note over User, UI : Sequence: Hand in Loan
            end
           

        Else Asset Status is Discontinued
            User ->> UI: Update Status
            Note over User, UI: Sequence: Update Asset from scan

        End
    
    
```