# ALoRa
C# Client for The Things Network Lora Applications

When I started looking at LoRa and LoRaWAN I found that there wasn't many C# examples out there showing how to connect to The Things Network (or others). The MQTT protocol language can be confusing to start with so here goes my attempt at making things simple!!

I've tried to follow and extend the model presented by The Things Network (TTN) by having and `TTNApplications` object and keep the initialisation in line with the TTN terminology. Once an application is created you will then start to receive `TTNMessage` objects for each message received via the `MessageReceived` event.

## ToDO
I want to add the ability to publish messages to the application soon and to allow more device orientated function such as just getting message for a specific device and being able to send messages to the device too. Any other ideas, please get in touch?

## Nuget
If you prefer to use Nuget to pull and utilise the library in your projects, you can download it here;

https://www.nuget.org/packages/Ekwus.ALoRa/

## Parameters
* AppID
...This is TNN application ID or name, it will generally be what you chose to call your application, NOT the EUI byte array.

* AppKey
...This is an application access key associated with your app. There is normally one called "default key" created for you, however you can and should add a specific key for any client you create so it can be revoked later if you need to. Do this in the Manage Keys section and limit the permissions to what you need.

* Region
...This gets prepended to the URL used to access TTN. Currently the URL is hardcoded and will end up as <region>.thethings.network. A typical region value is "eu" and you can find yours on the end of the applicatio Hanlder value which will be somthing like "ttn-handler-eu"

## Example
A simple console app can be created as follows (yes the this access a live TTN application, please don't abuse it);

```C#
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("\nALoRa ConsoleApp - A The Things Network C# Library\n");

        var app = new TTNApplication("therm-io", "ttn-account-v2._4qH38SGjH9glrlFITfokDMq4-csR65DrIRFryeZAGY", "eu");
        app.MessageReceived += App_MessageReceived;

        Console.WriteLine("Press return to exit!");
        Console.ReadLine();

        app.Dispose();

        Console.WriteLine("\nAloha, Goodbye, Vaarwel!");

        System.Threading.Thread.Sleep(1000);
    }

    private static void App_MessageReceived(TTNMessage obj)
    {
        var data = BitConverter.ToString(obj.Payload);
        Console.WriteLine($"Message Timestamp: {obj.Timestamp}, Device: {obj.DeviceID}, Topic: {obj.Topic}, Payload: {data}");
    }
}
```
