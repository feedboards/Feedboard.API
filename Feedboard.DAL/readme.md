### Scaffold-DbContext

navigate to project folder:

```
cd .\Feedboard.DAL\
```

run scaffolding:
_replace conneciton string with yours if needed_

```
dotnet ef dbcontext scaffold `
"Data Source=185.154.15.65;Initial Catalog=Feedboard;Persist Security Info=True;User ID=sa;Password=Aads@35Egtd@23; TrustServerCertificate=True;" `
Microsoft.EntityFrameworkCore.SqlServer `
`
--table AzureAccounts `
--table GitHubAccounts `
`
--data-annotations `
--context FeedboardDbContext `
--context-dir Context `
--output-dir Models `
--no-onconfiguring `
--force `
--verbose
```
	