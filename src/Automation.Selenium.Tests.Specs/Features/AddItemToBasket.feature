Feature: Add item to basket
	As a customer
	I want to add items to my basket
	So that I can checkout

Scenario: Adding an item to basket
	Given I am on an item page
	When I add the item to the basket
	Then I should see the following
		| Message                            | ItemCount | BasketTotal |
		| This item was added to your Basket | 1         | £15.00      |

Scenario: Verify my shopping bag
	Given I am on an item page
		And I add the item to the basket
	When I navigate to "Bag"
	Then I should see the following details
		| Total Quantity | Sub Total |
		| 1              | £15.00    |
		
