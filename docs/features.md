# Features
- Note: `Features are marked with this styling`
- There are linked sequence diagrams to the features and user stories. Some diagrams describes more than one user story.
## Epic: Administration of  Assets (CRUD operations)


### Create
`Tag new  Assets`
- As a user, I want to scan the serial number of an Asset, do an OCR and identify the serial number, then generate a QR code, which I can stick on the Asset, to make it easier to manage that Asset. 
- As a user, I want to retrieve metadata about Asset from external system, to make the creating process more automatic.
- As a user, I want to validate the Serial Number to the external system, to make sure the OCR identfied an actual Asset
    - [Sequence: Tag Asset with Serial Number](/sequence-diagrams/tag-new-asset.md#tag-asset-with-serial-number)
    - [test](./sequence-diagrams/tag-new-asset.md)
- As as user, I want to add  Assets without serial number, with generating a unique id and qr code and stick to asset, to make it easier to manage that asset. 
    - [Sequence: Tag Asset without Serial Number](/sequence-diagrams/tag-new-asset.md#tag-asset-without-serial-number)

- ***remove later*** https://github.com/dymosoftware/dymo-connect-framework (print javascript)


### Overview/Dashboard (read)
`View all  Assets in the system`
- As an user, I would like to view all Assets in a dashboard, to get an overview of my assets
    - [Sequence: View all Assets](/sequence-diagrams/read-asset.md#view-all-assets)
- As an user, I want to see all Assets based on category, to easier identify assets inm dashboard
    - [Sequence: View all Assets by Category](/sequence-diagrams/read-asset.md#view-assets-by-category)

`View a single Asset in the system`
- As a user, I would like to see a single Asset with all its information, to get more knowledge about the asset
    - [Sequence: View one Asset from table](/sequence-diagrams/read-asset.md#view-one-asset-from-table)
- As a user, I would like to see who has Asset if it is being lent out
    - [Sequence: View Loan by Asset](/sequence-diagrams/read-loan.md#view-loan-by-asset-id)


`Scan Asset and read information`
- As an user, I want to scan the the Asset's QR-code and view its information, to have easy acces to information
    - [Sequence: View one Asset from scan](/sequence-diagrams/read-asset.md#view-one-asset-from-scan)




### Update
`Update and add information to one or more  Assets at the same time`
- As an user, I want to change and add information to a Asset
    - [Sequence: Update Asset information](/sequence-diagrams/update-asset.md#update-asset-information)
- As an user, I want to change status on one or multiple Assets at once
    - [Sequence: Update Asset Status on one or more Assets](/sequence-diagrams/update-asset.md#update-asset-status-on-one-or-more-assets)
- As an user, I want to scan the resource and do simple updates on Asset.
    - [Sequence: Update Asset information and status from scan](/sequence-diagrams/update-asset.md#update-asset-information-and-status-from-scan)

### Delete
`Delete a "wrongly added" Asset`
- As an user, I want to delete Assets created by mistake, to make sure Assets table is not populated with unnecessary items
    - [Sequence: Delete wrongly added asset](/sequence-diagrams/delete-asset.md#delete-wrongly-added-asset)

`Deprecate an Asset`
- As an user, I want to change the status of the Asset to be unavailable/not usable, but still have the Asset in the system. I.e. the status should be changed from something to "discontinued"
    - [Sequence: Update Asset Status on one or more Assets](/sequence-diagrams/update-asset.md#update-asset-status-on-one-or-more-assets)

## Epic: Loans

### Loaning
`Scan Asset and lend out to employee`
- As a user, I want to scan the Asset, choose new loan and a time span and lend out to employee
- As a user, I want to scan the Asset, choose *no time limit*, lend out to employe
- As a user, when an Assey is lent out I want the system to send a notifcation to external system to mark the expense on employee, to reduce manual work
    - [Sequence: New Loan](/sequence-diagrams/loan-actions.md#new-loan)

- As a user I want to extend time of loan, to meet employees needs if requested
    - [Sequence: Extend Loan](/sequence-diagrams/loan-actions.md#extend-loan)


### Hand in Loan
`Scan Asset and hand in Asset`
- As a user, I want to scan the Asset and remove Loan from Loans table, so that Loans table only have *active loans*
    - [Sequence: Hand in Loan](/sequence-diagrams/loan-actions.md#hand-in-loan)

### LoanHistory
`Add Loan to history`
- As a system, I want the "finished" loan to be removed from **Loans table**, and added to **LoanHistory** table
    - [Sequence: Hand in Loan](/sequence-diagrams/loan-actions.md#hand-in-loan)


### Overview/Dashboard
`View all Loans`
- As a user, I want to view all loans in a table, to get an overview of Loans in system
    - [Sequence: View all Loans](/sequence-diagrams/read-loan.md#view-all-loans)

`View one loan`
- As a user, I want to view a single loan from table, to see information connected to that Loan
    - [Sequence: View one Loan from table](/sequence-diagrams/read-loan.md#view-one-loan-from-table)
- As a user, I want to scan an Asset and see the active loan connected to Asset, to get easy access on information
    - [Sequence: View one Loan from scan](/sequence-diagrams/read-loan.md#view-one-loan-from-scan)

`View all Loans linked to employee`
- As a  user, I want to look up employee numbers and get a list of Loans connected to this employee, to get an overview and easier manage employees Assets
    - [Sequence: View Loan by Employee](/sequence-diagrams/read-loan.md#view-loan-by-employee-id)

`View Loan History`
- As a user, I want to see previous loans in a seperate table, so that I can removed them from Loans table, but I still have the opportunity to track history
    - [Sequence: View Loan History](/sequence-diagrams/read-loan.md#view-loan-history)

### Notifications 
`Send notification`
- As a user, I want there to be an automatic email sent out to the employee when it's time to return the asset
    - [Sequence: Notification](/sequence-diagrams/notification.md#notify-employee-when-loan-is-due-to-be-delivered)

