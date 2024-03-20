# Security Issue Mitigation Report

**Author:** Joel Southall
**Course:** CS 296N
**Date:** March 20, 2024

## Security Issue Research Document

### 1. Content Security Policy (CSP) Header Not Set

**Research Findings:**
The absence of the Content Security Policy (CSP) header in HTTP responses exposes applications to Cross-Site Scripting (XSS) attacks and data injection risks. CSP adds a security layer that detects and mitigates XSS and data injection attacks.

**Mitigation in ASP.NET Core:**
Implemented CSP by adding middleware to the HTTP request pipeline in `Program.cs`:

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; script-src 'self'; object-src 'none';");
    await next();
});

##References:

    **MDN Web Docs - Content Security Policy (CSP)
    **OWASP - Content Security Policy

#2. X-Frame-Options Header Not Set

##Research Findings:
The X-Frame-Options HTTP response header protects against 'ClickJacking' attacks. Without this header, attackers can embed web pages into iframes on malicious sites, leading to unintended user actions and data compromise.

##Mitigation in ASP.NET Core:
Implemented the X-Frame-Options header in Program.cs:

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
    await next();
});

##References:

    MDN Web Docs - X-Frame-Options
    OWASP - Clickjacking Defense Cheat Sheet

#3. Spring4Shell Vulnerability

##Research Findings:
Spring4Shell is a critical vulnerability in the Spring Framework for Java, allowing remote code execution. This does not impact ASP.NET Core directly but is crucial for mixed technology environments.

##Mitigation in ASP.NET Core:
Ensured that no Java components are used or updated relevant components to secure versions. This was identified as a non-issue for the purely ASP.NET Core application but was investigated due to initial scan results.

##References:

    Spring Blog - Spring Framework RCE Mitigation
    SECURELIST - Spring4Shell Vulnerability

#Post-Mitigation Scan Results
##Manual Passive Scan Results:

  *  CSP: Wildcard Directive - Resolved
  *  Server Leaks Version Information - Unresolved; requires additional measures
  *  Cookie Without Secure Flag - Resolved
  *  Private IP Disclosure - Resolved
  *  Server Leaks Info via "X-Powered-By" - Resolved
  *  Session Management Response Identified - No action required (informational)
  *  Information Disclosure - Suspicious Comments - Unresolved; further review required
  *  Re-examine Cache-control Directives - Partially resolved; needs further refinement
  *  User Controllable HTML Element Attribute (Potential XSS) - Resolved

##Automated Passive Scan Results:

    CSP: Wildcard Directive - Resolved
    Application Error Disclosure - Resolved
    Cookie with SameSite Attribute None - Resolved
    X-Content-Type-Options Header Missing - Resolved

#Applied Fixes

In response to the identified issues, security headers were implemented in the middleware pipeline in Program.cs to enhance the application's security posture:

// Add custom security headers in the middleware pipeline
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; script-src 'self'; object-src 'none'; frame-ancestors 'none';");
    context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("Referrer-Policy", "no-referrer");
    await next();
});

##Security headers implemented:

  *  Content-Security-Policy
  *  X-Frame-Options
  *  X-Content-Type-Options
  *  Referrer-Policy

These measures address critical vulnerabilities, enhancing protection against XSS, clickjacking, and other web security threats.
