```mermaid
erDiagram
    Loan }o--|| Employee : has
    Loan ||--|| Asset : has
    Asset ||--|| LoanHistory : has
    Employee ||--|| LoanHistory : has
    Employee ||--o{ Notification : has    

```