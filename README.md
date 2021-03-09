# GraphQL Test

This is from me going through the blog posts at https://fiyazhasan.me/tag/graphql/

## Queries

```graphql
fragment itemFields on ItemType {
  __typename
  id
  title
  price
  tag
}

fragment customerFields on CustomerType {
  __typename
  name
  items {
    __typename
    tag
    createdAt
  }
  billingAddress
}

fragment orderFields on OrderType {
  __typename
  customer {
    ...customerFields
  }
  createdAt
  tag 
}

query GetCustomers {
  customers {
    ...customerFields
  }
}

mutation CreateCustomer($customer: CustomerInput!) {
  createCustomer(customer: $customer) {
    ...customerFields
  }
}

query GetItems {
  items {
    ...itemFields
  }
}

mutation CreateItems($item1: ItemInput!, $item2: ItemInput!, $item3: ItemInput!, $item4: ItemInput!, $item5: ItemInput!) {
  item1: createItem(item: $item1) {
    ...itemFields
  }
  item2: createItem(item: $item2) {
    ...itemFields
  }
  item3: createItem(item: $item3) {
    ...itemFields
  }
  item4: createItem(item: $item4) {
    ...itemFields
  }
  item5: createItem(item: $item5) {
    ...itemFields
  }
}

query GetLegion {
  item(tag: "wd3") {
    ...itemFields
  }
}

query GetOrders {
  orders {
    ...orderFields
  }
}

mutation CreateCustomerAndOrder($item: ItemInput!, $customer: CustomerInput!, $order: OrderInput!, $orderItem: OrderItemInput!) {
  createCustomer(customer: $customer) {
    ...customerFields
  }
  createOrder(order: $order) {
    ...orderFields
  }
  createItem(item: $item) {
    ...itemFields
  }
  addOrderItem(orderItem: $orderItem) {
    quantity
    item {
      ...itemFields
    }
    order {
      ...orderFields
    }
  }
}
```

## Items

```json
{
  "item": {
    "tag": "doom",
    "title": "Doom",
    "price": 9.99
  },
  "item1": {
    "tag": "cnc",
    "title": "Command & Conquer: Red Alert 3",
    "price": 9.99
  },
  "item2": {
    "tag": "control",
    "title": "Control",
    "price": 9.99
  },
  "item3": {
    "tag": "wd",
    "title": "Watch Dogs",
    "price": 9.99
  },
  "item4": {
    "tag": "wd2",
    "title": "Watch Dogs 2",
    "price": 9.99
  },
  "item5": {
    "tag": "wd3",
    "title": "Watch Dogs Legion",
    "price": 9.99
  },
  "order": {
    "tag": "OOR-123",
    "createdAt": "2020-08-25",
    "customerId": 1
  },
  "customer": {
    "name": "John Doe",
    "billingAddress": "123 Main St"
  },
  "orderItem": {
    "quantity": 1,
    "itemId": 1,
    "orderId": 1
  }
}
```