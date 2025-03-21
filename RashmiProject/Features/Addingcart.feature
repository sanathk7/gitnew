@Order3
Feature:Addingcart

Scenario: Adding Product to Cart
    Given User is logged in
    When User clicks on Add to Cart
    And User clicks on Cart
    Then User Cart should open
