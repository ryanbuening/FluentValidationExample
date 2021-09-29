using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;

namespace FluentValidationExample.Pages
{
	public class Form
	{
		public string PhoneNumber { get; set; } = "";

		public Contact Contact { get; set; } = new();
	}

	public class Contact
	{
		public string PhoneNumber { get; set; } = "";
	}

	public partial class Validation
	{
		public Form Form { get; set; } = new();
		public EditContext EditContext { get; set; } = default!;

		protected override void OnInitialized()
		{
			EditContext = new EditContext(Form);
		}

		public void Submit()
		{
			var formIsValid = EditContext!.Validate();
			Console.WriteLine("formIsValid= " + formIsValid);
		}
	}

	public class FormValidator : AbstractValidator<Form>
	{
		public FormValidator()
		{
			RuleFor(form => form.PhoneNumber).MinimumLength(10); // works as expected and will show the validation onchange of field

			RuleFor(form => form.Contact.PhoneNumber).MinimumLength(10); // does not work as expected and only shows validation when form is submitted

			//RuleFor(form => form.Contact).SetValidator(new ContactValidator());
		}
	}

	public class ContactValidator : AbstractValidator<Contact>
	{
		public ContactValidator()
		{
			//RuleFor(contact => contact.PhoneNumber).MinimumLength(10); // works as expected and will show the validation onchange of field
		}
	}
}
