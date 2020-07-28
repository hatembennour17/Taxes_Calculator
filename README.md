Name: Hatem Bennour
Choice: Sales Taxes
used Vs 2019: console app core, xunit core test project.
orders are passed in as arguments: right click project --> properties--> select debug from left menu --> scroll to application arguments.
inputs are ";" delimiter and items are :,: separated.

Note:

I wrote this code with OOP and SOLID principals in mind, I wrote 2 classes one for Sales Taxes calculation and one class for Import taxes calculation ( separation or concerns) I creates an Interface for each class (interface segregation principal) for 2 reasons: Extensibility and easy xUnit test implementation .
If in the future we need to add calculation shipping cost or a discount calculation for example based on a business rules I can just add a new class/interface for each calculation this is open/closed principal
classes should be closed for modifications but open for extensions.

Thank you for taking the time to review my code.

Hatem Bennour.
