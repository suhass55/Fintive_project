Feature: FinitiveDataValidationUser
	#InvoiceCodingRule, LoginandLogout, MyAccountDataValidation, UserAdminFeature, Covid19, TaxExDataValidation

	@312 @Regression @Smoke
Scenario: 312: Finitive_Signup process in the Application
	Given Navigate to Finitive url And Verify
	Then Click On Signup Button
	Then Enter Signup Details
	Then Click On Signup
	Then Click On Verify Button From Registered Email