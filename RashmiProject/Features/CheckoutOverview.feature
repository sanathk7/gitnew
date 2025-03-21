@CheckoutOverview
Feature: 5CheckoutOverview

Scenario: Checkout Overview and Order Confirmation
    Given User is on the Checkout Overview page
    When User clicks on Finish
    Then Order status should be visible

