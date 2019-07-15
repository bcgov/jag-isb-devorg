# jag-isb-devorg
Ministry of Attorney General ISB Dynamics-OpenShift DevOrg

## Purpose ##
This repository is an example of how to connect to Microsoft Dynamics from within software deployed to the OpenShift environment. 


Technology Stack
-----------------

| Layer   | Technology | 
| ------- | ------------ |
| Framework | Dotnet Core 2.2 |
| Presentation | Razor / JSON API responses |
| Business Logic | C# - Dotnet Core 2.2 Web MVC |
| Web Server | Kestrel |
| Authentication, including OAUTH2 and SAML | ADFS 2016, On Premise |
| Data | Dynamics 365 CE Version 9, On Premise |
| File Storage | SharePoint 2016, On Premise |


Developer Prerequisites
-----------------------

**Portal Application and Example Dynamics Library**
- .Net Core SDK (Dotnet Core 2.2 is used for all components)
- .NET Core IDE such as Visual Studio 2019 or VS Code

**DevOps**
- RedHat OpenShift tools
- Docker
- A familiarity with Jenkins


Configuration
-----------------------
Configure the following secrets in your development or deployment environment:

| Secret Name | Description |
| ----------- | ------------|
| ADFS_OAUTH2_URI | ADFS OAUTH2 URI - usually /adfs/oauth2/token on your STS server. |
| DYNAMICS_ODATA_URI | Endpoint for the Dynamics REST interface.  May be an API gateway URL.  |
| DYNAMICS_APP_GROUP_RESOURCE | ADFS 2016 Application Group resource (URI) |
| DYNAMICS_APP_GROUP_CLIENT_ID | ADFS 2016 Application Group Client ID |
| DYNAMICS_APP_GROUP_SECRET | ADFS 2016 Application Group Secret |
| DYNAMICS_USERNAME | Service account username.  Format is username@domain where domain is the Active Directory domain. |
| DYNAMICS_PASSWORD | Service account password |
| SHAREPOINT_ADFS_TOKEN_URI | URI that will be used to get a SAML token  |
| SHAREPOINT_RELYING_PARTY_IDENTIFIER | URN for the relying party.  Matches that used for interactive login. |
| SHAREPOINT_USERNAME | Username for the Service Account that will be used to access SharePoint.  In most cases this will be the same as that used for Dynamics. |
| SHAREPOINT_PASSWORD | Password for the Service Account that will be used to access SharePoint. |


Service Account Setup
-----------------------
- Do not use the same service account for multiple environments
- Do not use the same password for multiple service accounts
- If deploying to Production, ensure that the service account has an appropriate setting for the password expiry
- Create a Dynamics Role for OpenShift access
- Assign correct permissions to the OpenShift access role.  For example, if your public facing code will create, edit and display Contacts, ensure that the role has sufficient permissions to create, read and edit a Contact and any related entities.
- Add the Service Account to the Dynamics instance
- Assign the OpenShift access role to the service account in Dynamics   


Troubleshooting
---------------

Fiddler, Wireshark or similar traffic analysis tools are essential for troubleshooting authentication issues.
- If you are getting a 401 or 403 error, check that all of your credentials are correct
- The SharePoint relying party identifier can be obtained by running fiddler and doing an interactive login.  Examine the Fiddler logs to see what relying party was passed to the adfs server (typically the "sts").  The relying party will typcially start with urn: however it may also start with https://

 
Contribution
------------

Please report any [issues](https://github.com/bcgov/jag-isb-devorg/issues).

[Pull requests](https://github.com/bcgov/jag-isb-devorg/pulls) are always welcome.

If you would like to contribute, please see our [contributing](CONTRIBUTING.md) guidelines.

Please note that this project is released with a [Contributor Code of Conduct](CODE_OF_CONDUCT.md). By participating in this project you agree to abide by its terms.

License
-------

    Copyright 2019 Province of British Columbia

    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at 

       http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.

Maintenance
-----------

This repository is maintained by [BC Attorney General]( https://www2.gov.bc.ca/gov/content/governments/organizational-structure/ministries-organizations/ministries/justice-attorney-general ).
