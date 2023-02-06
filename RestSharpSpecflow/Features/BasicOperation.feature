Feature: BasicOperation
Covers basic API Test operations

@smoke
Scenario: GET products
	Given I perform a GET operation of "Product/GetProductById/{id}"
		| ProductId |
		| 1         |
	And I should get the product name as "Keyboard"