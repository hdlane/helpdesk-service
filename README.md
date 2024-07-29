# Helpdesk Service

## Description

Helpdesk Service is a Windows service built in C# that queries devices for information which is then sent to a configured endpoint. It is intended to be used in conjunction with the [Helpdesk Tool](https://github.com/hdlane/helpdesk-tool) and [Helpdesk API](https://github.com/hdlane/helpdesk-api).

### Goal

The goal of the Helpdesk Service is to get information from computers and in front of helpdesk teams to make support quick and easy.

### Background

While developing the Helpdesk Tool, I wanted to get as close to live data as possible with Windows computers on the network. This prompted me to develop a Windows service that sends that information to an endpoint more frequently and only when the computer's information changed.

### Features

* Query computer information periodically that is sent to an endpoint only when information changes
* Install as a Windows service that will run no matter who is logged in
* Logging is sent to the Windows Event Viewer

## Installation

### Requirements

## Usage

## License

[MIT](https://choosealicense.com/licenses/mit/)
