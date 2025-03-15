### Overview
This is a simple application to test Ubiquiti's UniFi API.

### Configuring the API Key
To run this program, you'll need a UniFi API key. To create one:

* Open your Site in UniFi Site Manager at unifi.ui.com.
* Locate your device (e.g. a UniFi Dream Machine)
* Within that device, click on Control Plane, then Admins & Users.
* Select your Admin.
* Click Create API Key.
* Add a name for your API Key.
* Copy the key and store it securely, as it will only be displayed once.
* Click Done to ensure the key is hashed and securely stored.
* Use the API Key.

Next, you'll need to store this API Key so this program can find it. To do so:
* Run `cd BitrateUtility/BitrateUtility/`
* Run `dotnet user-secrets set "UniFiAPIKey" "<Your API Key>"`