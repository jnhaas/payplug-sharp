Welcome to PayPlug e-commerce library's documentation!
======================================================

This is the documentation of PayPlug's e-commerce .NET library. It is designed to
help developers to use PayPlug as payment solution in a simple, yet robust way.

Prerequisites
-------------

PayPlug's library relies on **Newtonsoft.Json** to JSON serialization. You also need **.NET 4.5** or newer for the PayPlug.


Installation
------------

**Option 1 - Strongly preferred)** via NuGet:

.. code-block:: bash

    PM> Install-Package Payplug 


or simply add *payplug* as a dependency of your project.

**Option 2)** download as a tarball:

- Download the most recent stable tarball from the `download page`__
- Extract the tarball somewhere outside your project.
- *chdir* into the folder you've just extracted.
- Run the following commands:

.. code-block:: bash

    $ rake compile

__ https://github.com/payplug/payplug-sharp/releases

To get started, add the following piece of code to the header of your source:

.. sourcecode:: c#

    using Payplug;

If everything run without error, congratulations. You installed PayPlug .NET library! You're ready to create your
first payment.

Usage
-----

Here's how simple it is to create a payment request:

.. sourcecode :: c#

    var paymentData = new Dictionary<string, dynamic>
    {
	    {"amount", 3300},
		{"currency", "EUR"},
		{"customer", new Dictionary<string, object>
		    {
			    {"email", "john.watson@example.net"},
				{"first_name", "John"},
				{"last_name", "Watson"}
		    }
		},
		{"hosted_payment", new Dictionary<string, object>
		    {
                {"return_url", "https://example.net/success?id=42710"},
                {"cancel_url", "https://example.net/cancel?id=42710"}
            }
        },
        {"notification_url", "https://example.net/notifications?id=42710"},
        {"metadata", new Dictionary<string, object>
            {
                {"customer_id", "42710"}
            }
		},
		{"save_card", false},
		{"force_3ds", true}
    };
    var payment = Payment.Create(paymentData);


Go further:
-----------
Documentation:
++++++++++++++

https://www.payplug.com/docs/api/?python

Tests:
++++++
To run the tests, run the following command:

.. code-block:: bash

    $ rake test
