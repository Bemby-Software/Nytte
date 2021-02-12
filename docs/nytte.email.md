# Nytte Email
## Summary
Nytte Email is an email service that wraps MailKit. It currently provides support for razor email templatec (when using Nytte.Email.Razor).

## Initialising Nytte.Email
### Using appsettings.json
Ensure you are storing the configuration instance.
```C#
public IConfiguration Configuration { get; }
public Startup(IConfiguration configuration)
{
    Configuration = configuration;
}
```

In the configure services method add the following.
```C#
services.NytteEmailConfigureSmtpServer(Configuration)
        .AddNytteEmail()
        .AddNytteRazorEmails(); // Omit this if you do not need the Razor email builder
```

In the configure method add the following.
```C#
app.UseNytteEmails()
   .UseNytteRazorEmails(); // Omit this if you do not need the Razor email builder
```

In appsettings.json, ensure that the following is configured.
```JSON
    "SmtpServerConfiguration": {
        "ServerAddress": "address",
        "ServerPort": "port",
        "ServerUserName": "username",
        "ServerPassword": "password",
        "ConnectionSocketOptions":  3
    },
```

The following options are available for ConnectionSocketOptions.

[As taken from the mimekit documentation.](http://www.mimekit.net/docs/html/T_MailKit_Security_SecureSocketOptions.htm)

|Enum Option|Enum Value|Description|
|-----------|----------|-----------|
|None|0|No SSL or TLS encryption should be used.|
|Auto|1|Allow the IMailService to decide which SSL or TLS options to use (default). If the server does not support SSL or TLS, then the connection will continue without any encryption.|
|SslOnConnect|2|The connection should use SSL or TLS encryption immediately.|
|StartTls|3|Elevates the connection to use TLS encryption immediately after reading the greeting and capabilities of the server. If the server does not support the STARTTLS extension, then the connection will fail and a NotSupportedException will be thrown.|
|StartTlsWhenAvailable|4|Elevates the connection to use TLS encryption immediately after reading the greeting and capabilities of the server, but only if the server supports the STARTTLS extension.|