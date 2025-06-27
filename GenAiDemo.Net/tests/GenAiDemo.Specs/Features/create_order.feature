Feature: Create Order
  Scenario: Create simple order
    Given an order item
    When the order is created
    Then the result should contain an id
