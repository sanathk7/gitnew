@Order1
Feature:1LoginPages

Scenario Outline:1A Verify Login with Valid Credentials
    Given User is on the Login Page
    When User enters the Username "<firstname>"
    And User enters the Password "<password>"
    And Clicks the Login button
    Then Homepage should open

Examples:
    | firstname     | password     |
    | standard_user | secret_sauce |
    

