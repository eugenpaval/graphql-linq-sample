# graphql-linq-sample
An implementation of a GraphQL server (.NET) with a schema inspired by Linq
Implemented as an ASP.NET Core server with two controllers
One Controller (Admin) exposes a graphiql page allowing interactive queries
The other controller is the API controller to make graphql queries against (api/graph)

After you build the server, run it (CTRL-F5) and enter a query like

```
query Test
{
  products
  {
    where(exp: "category == \"Category-1\" or category == \"Category-2\"")
    {
      orderBy(exp: "category, name desc")
      {
        select
        {
          name
          category
          price
        }
      }
    }
  }
}
```
or
```
query Test
{
  products
  {
    where(exp: "price > 50")
    {
      orderBy(exp: "category, name desc")
      {
        select
        {
          name
          category
          price
        }
      }
    }
  }
}
```
