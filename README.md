# Helpdesk Service

## Description

Helpdesk Service is a Windows service built in C# that queries devices for information which is then sent to a configured endpoint. It is intended to be used in conjunction with the [Helpdesk Tool](https://github.com/hdlane/helpdesk-tool) and [Helpdesk API](https://github.com/hdlane/helpdesk-api).

### Goal

The goal of the Helpdesk Service is to get information from computers and in front of helpdesk teams to make support quick and easy.

### Background

While developing the Helpdesk Tool in-house, there were a couple different systems that were being used to gather information to populate the application. One of those systems was an inventory server that was limited on how often it could scan PCs to get information (like the current user logged in). This led to issues when users logged into their PC in between scans and the Helpdesk Tool wasn't up-to-date. This prompted me to develop a Windows service that sends that information to an endpoint more frequently and only when the computer's information changed.

### Features

* Query computer information periodically that is sent to an endpoint only when information changes
* Install as a Windows service that will run no matter who is logged in
* Logging is sent to the Windows Event Viewer

## Installation

### Requirements

## Usage

## License

[MIT](https://choosealicense.com/licenses/mit/)
