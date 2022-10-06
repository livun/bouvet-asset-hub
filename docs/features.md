# Feautures Suggestions
## Epic: Administration (CRUD operations)
Note: `Features are marked with this styling`

### Create
`Tag new asset`
- Ass a admin user, I want to scan the serial number of asset, do an OCR and identify the serial number, then generate a QR code, which I can stick on the asset.
- As as admin user, I want to add assets without serial number, with generating a unique id and qr code and stick to asset.
- As a user, I want to mark my new asset with a QR, so that it is easy to identify
- https://github.com/dymosoftware/dymo-connect-framework (print javascript)

`Register required information`
- As a admin user, I want to retrieve required information about asset from external assets, to make the creating process more automatic.
- As a admin user, I want to manually enter nessecary information if the option to retrieve from external asset is not avaible. 

### Overview/Dashboard (read)
`View all assets in the system`
- As an admin user, I would like to view alle assets in a dashboard
- As an admin user, I want to see all assets based on sorting values (status, catgory, date)
- As a regular user I want to see all the assets I have checked out

`View a single asset in the system`
- As an admin user, I would like to see a single asset with all its information

`Scan asset and read information`
- As an admin user, I want to scan the the asset's QR-code and view its information

`View all assets linked to employee`
- As an admin  user  , I want to look up employee numbers and get a list of assets connected to this user

### Update
`Update and add information to one or more assets at the same time`
- As an admin user, I want to change and add information to a asset
- As an admin user, I want to change/add information multiple assets at once
- As an admin user, I want to change the status of a asset
- As an admin user, I want to change the status of several assets at the same time
- As an admin user, I want to scan the resource and do simple updates on asset.

### Delete
`Delete a "wrongly added" asset`
- As an admin user, I want to delete assets added by mistake

`Deprecate an asset`
- As an admin user, I want to change the status of the asset to be unavailable/not usable, but still have the asset in the system. I.e. the status should be changed from something to "discontinued"

## Epic: Loans

### Checkout
`Scan asset and check out to employee`
- As an admin user, I want to scan the asset, choose the time span, check out to employee
- As an admin user, I want to scan the asset, choose *no time limit*, check out to employee

### Check in
`Scan asset and check in asset`
- As an admin user, I want to scan the asset and check in the asset


### Notification 
`Send notification`
- As a user, I want there to be an automatic email sent out to the employee when it's time to return the asset

### LoanHistory
`Add loan to history`
- As a system, I want the "finished" loan to be removed from **Loans table**, and added to **LoanHistory** table
