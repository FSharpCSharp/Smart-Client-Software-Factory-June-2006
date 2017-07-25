# Smart Client Software Factory –June 2006

#Foreword

This repository contains a portion of the Smart Client Software Factory from 2006. Most of the projects have been migrated. Many things can not be easily transferred to today's time. For this reason, not everything is fully operational. It is therefore to be viewed as a relict of past times. Perhaps one or the other is still useful today.

This document contains late-breaking information about Smart Client Software Factory – June 2006. Any additional information and bug fixes that are delivered after the release date will be available on the Smart Client Software Factory Community. 
## Known Issues
The following sections contain information about this release.
###Guidance Package
Dropdown fields in Add Event Subscription and Add Event Publication wizards appear read-only. The wizard pages for the Add Event Subscription and Add Event Publication recipes contain a dropdown field for the publication scope. This field appears as read-only; however, you can use the dropdown to change the value.
Using the Add Event Publication recipe on a sealed class will result in build errors with the generated code. The recipe adds a virtual method to the class; sealed classes cannot contain virtual methods. 
When you choose a generic type for the Event argument type field in the Add Event Publication and Add Event Subscription wizards, the generated code fails to compile. The recipe does not recognize that you specified a generic event argument type. To avoid compilation errors, specify a concrete type for the event argument type.
The guidance package recipes do not validate identifiers for event topic names, module names, and solution names. You must use a valid C# identifier for these fields. If you do not, the guidance package generates code that does not compile.
###Guidance Navigator
You can use the Guidance Navigator to add a module to any folder or class file. If you choose any other target than a solution folder or the solution root, you will receive an error message. You should only add a module to the solution root or a solution folder.
Guidance Navigator displays the word Add twice for bound recipes. The recipe reference is valid; it is only the display text that is incorrect.
Bound recipes do not appear in the History column of the Guidance Navigator. If you run bound recipes, such as Add Business Module or Add Foundational Module, you will not see log entries in the History column.
###Appraiser Workbench Reference Implementation
The file AppraisalManagementService.cs contains a hard-coded URL for the AppraisalService. If you change the location of the Web service, you must update this source file. 
