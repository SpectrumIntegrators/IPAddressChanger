# IP Address Changer

## Overview
This application allows changing IP address settings for network adapters from a quick list of predefined configurations. IP addresses may be IPv4 or IPv6, or DHCP can be enabled. (IPv6 has not been tested.)

The application requires elevated privileges and may only be run by a user who has administrator acces to the system.

The network adapter settings functionality is provided by PowerShell. If PowerShell is restricted or disabled, the application will not function properly.

## Table of Contents
1. [Shortcuts](#shortcuts)
1. [Main Window](#main-window)
    1. [Tool Bar](#tool-bar)
    1. [Status Bar](#status-bar)
    1. [Adapters List](#adapters-list)
    1. [Shortcuts List](#shortcuts-list)
    1. [Adapter Details](#adapter-details)
    1. [Adapter Address List](#adapter-addresses-list)
1. [Notification Area Icon](#notification-area-icon)
1. [New/Edit Shortcut Window](#newedit-shortcut-window)
1. [Settings Window](#settings-window)
1. [Debug Messages Window](#debug-messages-window)
1. [Copyright](#copyright)

## Shortcuts
The functionality of the program revolves around Shortcuts, which are configuration presets for specific adapters. If the adapter name is changed, the shortcut should still work (it's based on its device ID). The adapter must be present for the shortcut to work, but the current adapter index does not matter because the shortcuts are based on the device ID.

## Main Window
![Main Window](https://spectrumintegrators.github.io/IPAddressChanger/images/main.png)

### Layout
The main window is divided into a [Tool Bar](#tool-bar), a [Status Bar](#status-bar), and four areas: [Adapters List](#adapters-listadapters), [Shortcuts List](#shortcuts-list), [Adapter Details](#adapter-details), and [Adapters Address List](#adapter-addresses-list). The relative sizes of each area may be changed by dragging the bar between the areas (sizes are saved on exit).

### Tool Bar

#### Refresh
Refresh the list of adapters.

#### Hide Offline Adapters
Toggles hiding and showing offline adapters.

#### New Shortcut
Creates a new address configuration shortcut for the currently selected network adapter.

#### Delete Shortcut
Deletes the currently selected shortcut.

#### Edit Shortcut
Edits the currently selected shortcut.

#### Recall Shortcut
Sets the adapter configuration information for the adapter referened in the curently selecte shortcut.

#### Settings
Displays the settings window.

#### Debug
Displays the debug messages window.

#### Control Panel
Launches the network adatpers control panel.

### Status Bar

### Adapters List
This list shows all of the network adapters present in the system. Each line shows the current adapter index (this may changed after a reboot but it doesn't affect the shortcuts), the adapter name, and its up/down state.

Selecting an adapter will display additional details in the [Adapter Details](#adapter-details) area and associated addresses in the [Adapter Addresses List](#adapter-addresses-list).

Double-clicking an adapter will create a new shortcut for that adapter.

### Shortcuts List
This list shows all of the stored configuration preset shortcuts.

Double-clicking a shortcut will either edit the shortcut or recall the shortcut, depending on the value of the [Start minimized](#start-minimized) setting.

These shortcuts are also available in the Shortcuts menu of the [Notification Area Icon Menu](#notificaion-area-icon-menu).

### Adapter Details

#### Hardware Address
The unformatted hardware address of the adapter (usually the MAC address for an Ethernet device)

#### Speed
The one-way data rate of the adatper in scaled bits per second (bits, kilobits, megabits, etc.).

#### Driver Description
The description of the driver that is used to interface with the adapter hardware.

#### Device ID
The unique identifier of this adapter.

### Adapter Addresses List
This shows all of the addresses configured for the selected adapter. You may resize and rearrange the columns (size and position will be saved on exit).

Double-clicking an address will create a shortcut for the adapter using that address and prefix length or DHCP.

#### Address
The address value for this address.

#### Prefix Length
The number of bits of the address that are used for the network segment.

#### Family
The address family this address uses.

#### Prefix Origin
The origin of the network portion of the address.

#### Suffix Origin
The origin of the host  portion of the address.

## Notification Area Icon
The notification area icon appears in the notification area of the task bar near the clock (you may need to click the expand button to see it). Double-clicking the icon will show the application. Right clicking it will show a quick-access menu.

![Notification Area](https://spectrumintegrators.github.io/IPAddressChanger/images/notificationmenu.png)

### Notificaion Area Icon Menu

#### Show
Shows the application.

#### Hide
Hides the application (it will not appear on the taskbar, but the icon remains in the notification area).

#### Exit
Closes the application.

#### Control Panel
Launches the network adapters control panel.

#### Shortcuts
This menu contains all of the configured network adapter shortcuts. It is the same list as the [Shortcuts List](#shortcuts-list). Clicking one of these items will recall that shortcut.

## New/Edit Shortcut Window
![Shortcut Window](https://spectrumintegrators.github.io/IPAddressChanger/images/shortcut.png)

### Shortcut Name
This is the name you will see in the [Shortcuts List](#shortcuts-list) and in the [Notification Area Icon Menu](#notificaion-area-icon-menu).

When creating a new shortcut, this name will be automatically generated, but once you edit it manually it will retain whatever value you provide.

### Adapter
The name and device ID of the adapter this shortcut applies to, for reference.

### DHCP Enabled
Check this to use DHCP for this shortcut instead of specifying an address (the IP Address and Prefix Length fields are disabled while the DHCP checkbox is checked).

### IP Address
The IP address to use for this shortcut.

### Prefix Length
The length of the network portion of the address to use for this shortcut. For IPv4 addresses, the subnet mask is automatically displayed as well.

### Delete Button
When editing an existing shortcut, you can instead choose to delete the shortcut from this window.

## Settings Window
This window allows changing the functionality of the program.

![Settings Window](https://spectrumintegrators.github.io/IPAddressChanger/images/settings.png)

### Hide when minimized
Hides the window from the task bar when it is minimized (it can be shown again from the [Notification Area Icon Menu](#notificaion-area-icon-menu)).

### Start minimized
Automatically minimizes the application as soon as it launches. (If Hide when minimized is checked, it will also be hidden at launch.) The [Notification Area Icon](#notification-area-icon) is still available for shortcuts and to show the application.

### Double clicking a shortcut will
Chooses what action to perform when a shortcut from the [Shortcuts List](#shortcuts-list) is double-clicked: edit the selected shortcut, or recall the selected shortcut.

### Control Panel file
Path to the network adapters control panel file. This should be able to be left to the default value (`ncpa.cpl`), but if for some reason that file is moved or not available on your system, any other file may be specified here and it will be launched when the [Control Panel](#control-panel) button is clicked.

## Debug Messages Window
This window shows additional information about the actions the program is performing and the results of those actions.

![Debug Messages Window](https://spectrumintegrators.github.io/IPAddressChanger/images/debug.png)

### Tool Bar

#### Clear Log
Clears all entries from the debug messages log.

#### Copy Messages
Copies all of the selected debug messages to the clipboard.

#### Save Log
Saves the entire log to a file.

### Debug Messages
This list contains all of the debug messages. Clicking a line will select it, using `CTRL` and `Shift` will allow selecting multiple items.

`CTRL+C` will copy selected items to the clipboard. `CTRL+A` will select all items.

## Copyright
	IP Address Changer - Windows GUI application to quickly change network address settings.
    Copyright (C) 2024 Jonathan Dean (jonathand at spectrumintegrators.com)

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.