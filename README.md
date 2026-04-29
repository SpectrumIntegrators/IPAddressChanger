# IP Address Changer

## Overview
This application allows changing IP address settings for network adapters from a quick list of user-defined configurations or copy/paste.

The application requires [elevated privileges](#privilege-elevation--uac-prompt) and may only be run by a user who has administrator access to the system.

## Table of Contents
1. [Shortcuts](#shortcuts)
1. [Main Window](#main-window)
    1. [Main Tool Bar](#main-tool-bar)
    1. [Status Bar](#status-bar)
    1. [Adapters Tool Bar](#adapters-tool-bar)
    1. [Shortcuts Tool Bar](#shortcuts-tool-bar)
    1. [Adapters List](#adapters-list)
        1. [Adapter Context Menu](#adapter-context-menu)
    1. [Shortcuts List](#shortcuts-list)
        1. [Shortcut Context Menu](#shortcut-context-menu)
    1. [Adapter Details](#adapter-details)
    1. [Adapter Addresses List](#adapter-addresses-list)
        1. [Adapter Addresses Context Menu](#adapter-addresses-context-menu)
1. [Notification Area Icon](#notification-area-icon)
1. [New/Edit Shortcut Window](#newedit-shortcut-window)
1. [Adapter Busy Dialog](#adapter-busy-dialog)
1. [Address Conflict Warning](#address-conflict-warning)
1. [Settings Window](#settings-window)
1. [Debug Messages Window](#debug-messages-window)
1. [Privilege Elevation & UAC Prompt](#privilege-elevation--uac-prompt)
1. [Windows SmartScreen Warning](#windows-smartscreen-warning)
1. [Things I Haven't Tested](#things-i-havent-tested)
1. [AI Disclosure](#ai-disclosure)
1. [Copyright](#copyright)

## Shortcuts
The functionality of the program revolves around Shortcuts, which are configuration presets for specific adapters. If the adapter name is changed, the shortcut should still work (it's based on its device ID). The adapter must be present for the shortcut to work, but the current adapter index does not matter because the shortcuts are based on the device ID.

## Main Window
![Main Window](./images/main.png)

### Layout
The main window is divided into and four areas: [Adapters List](#adapters-list), [Shortcuts List](#shortcuts-list), [Adapter Details](#adapter-details), and [Adapters Address List](#adapter-addresses-list). The relative sizes of each area may be changed by dragging the bar between the areas (sizes are saved on exit).

### Main Tool Bar

![Main Tool Bar](./images/maintoolbar.png)

#### Settings
Displays the settings window.

#### Debug
Displays the debug messages window.

#### Control Panel
Launches the network adapters control panel.

#### Help
Shows the program documentation.

#### Feedback
Launches a browser window to submit bug reports and feedback.

### Status Bar

![Status Bar](./images/statusbar.png)

Shows what the software is currently doing or the results of the last operation, as well as the current version of the software.

### Adapters Tool Bar

![Adapters Tool Bar](./images/adapterstoolbar.png)

#### Refresh
Refresh the list of adapters.

#### Hide Offline Adapters
Toggles hiding and showing offline adapters.

### Shortcuts Tool Bar

![Shortcuts Tool Bar](./images/shortcutstoolbar.png)

#### New Shortcut
Creates a new address configuration shortcut for the currently selected network adapter.

#### Delete Shortcut
Deletes the currently selected shortcut.

#### Edit Shortcut
Edits the currently selected shortcut.

#### Recall Shortcut
Sets the adapter configuration information for the adapter referenced in the currently selected shortcut.

#### Move Shortcut Up
Moves the shortcut up in the list of shortcuts.

#### Move Shortcut Down
Moves the shortcut down in the list of shortcuts.

### Adapters List

![Adapters List](./images/adapterslist.png)

This list shows all of the network adapters present in the system. Each line shows the current adapter index (this may changed after a reboot but it doesn't affect the shortcuts), the adapter name, and its up/down/disabled state. (The icons correspond to up, down, or disabled; they look awful right now, someday they'll look better.)

Selecting an adapter will display additional details in the [Adapter Details](#adapter-details) area and associated addresses in the [Adapter Addresses List](#adapter-addresses-list). Selecting a disabled adapter will only show "Adapter disabled" in the addresses list.

Double-clicking an adapter will create a new shortcut for that adapter.

#### Adapter Context Menu

![Adapter Context Menu](./images/adaptercontextmenu.png)

Right-clicking an adapter shows a context menu with the following items:

##### New Shortcut
Creates a new empty shortcut for this adapter.

##### New Shortcut with...
Pre-fills the new-shortcut dialog with this adapter's currently configured method — either `DHCP` (if the adapter is set to acquire its address via DHCP) or the adapter's first IPv4 address and prefix length. Disabled when the adapter has neither (e.g. an adapter with no IPv4 configuration). The menu label shows what will be pre-filled, e.g. *New Shortcut with 10.0.0.69/16*.

##### Renew DHCP Lease
Same action as the [Renew DHCP Lease](#renew-dhcp-lease) button in the [Adapter Details](#adapter-details) area. Enabled only when the adapter has IPv4 DHCP configured.

##### Paste *value*
Applies an IP/CIDR or `DHCP` value from the clipboard directly to this adapter, with the same workflow as recalling a shortcut. The menu label shows the value that will be pasted, e.g. *Paste 10.0.0.69/16* or *Paste DHCP*. Disabled when the clipboard does not contain a valid IPv4/CIDR or `DHCP` string.

Note that the address in the clipboard does not have to have come from this software, if you copy a valid IP address and network prefix in CIDR notation or the literal string `DHCP`, the software will use that value to paste. If the IP address is invalid (like `123.456.789.12`), the paste feature will ignore it. Addresses with leading zeroes in an octet (`010.001.001.001`) will also be ignored.

A pre-apply check refuses to assign an IP that is already in use on a different adapter on the same system, before any changes are made to this adapter.

### Shortcuts List

![Shortcuts List](./images/shortcutslist.png)

This list shows all of the stored configuration preset shortcuts.

Double-clicking a shortcut will either edit the shortcut or recall the shortcut, depending on the value of the [Start minimized](#start-minimized) setting.

These shortcuts are also available in the Shortcuts menu of the [Notification Area Icon Menu](#notificaion-area-icon-menu).

#### Shortcut Context Menu
![Shortcut Context Menu](./images/shortcutcontextmenu.png)

Right-clicking a shortcut shows a context menu with the following items:

##### New
Creates a new shortcut.

##### Delete
Deletes the selected shortcut.

##### Edit Shortcut
Opens the [New/Edit Shortcut Window](#newedit-shortcut-window) for the selected shortcut.

##### Recall
Applies the selected shortcut to its associated adapter.

##### Copy *value*
Copies the shortcut's value (IP/CIDR or `DHCP`) to the clipboard. The clipboard contents can then be pasted onto any adapter via the [Paste](#paste-value) item on the adapter context menu.

### Adapter Details

![Adapter Details](./images/adapterdetails.png)

#### Hardware Address
The unformatted hardware address of the adapter (usually the MAC address for an Ethernet device)

#### Speed
The one-way data rate of the adapter in scaled bits per second (bits, kilobits, megabits, etc.).

#### Driver Description
The description of the driver that is used to interface with the adapter hardware.

#### Device ID
The unique identifier of this adapter.

#### Renew DHCP Lease
Releases and renews the DHCP lease on this adapter. Enabled only when the adapter has IPv4 DHCP configured. The renew operation typically completes in well under a second when a DHCP server responds, but may take 30–60 seconds when the network is unreachable or no server replies. While the process is underway, the [Adapter Busy Dialog](#adapter-busy-dialog) is displayed. It can be dismissed while the process continues in the background (additional operation on this adapter will be blocked until the process is complete). DHCP renewal errors will be shown in the [debug window](#debug-messages-window) and also in a popup dialog.

![DHCP renewal error example](./images/dhcprenewerror.png)

![DHCP not available error](./images/dhcprenewerror2.png)

After a successful renew, the status bar suggests using [Refresh](#refresh) to update the address list with the new lease.

### Adapter Addresses List

![Adapter Addresses List](./images/adapteraddresslist.png)

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
The origin of the host portion of the address.

#### Adapter Addresses Context Menu

![Adapter Addresses Context Menu](./images/adapteraddressesmenu.png)

##### New Shortcut with *value*
Opens the new shortcut dialog pre-filled with the selected address' values (or `DHCP`).

##### Copy *value*
Copies the selected addresses' values (or `DHCP`) to the clipboard.

## Notification Area Icon
The notification area icon appears in the notification area of the task bar near the clock (you may need to click the expand button to see it). Double-clicking the icon will show the application. Right clicking it will show a quick-access menu.

![Notification Area](./images/notificationmenu.png)

### Notification Area Icon Menu

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
![Shortcut Window](./images/shortcut.png)

### Shortcut Name
This is the name you will see in the [Shortcuts List](#shortcuts-list) and in the [Notification Area Icon Menu](#notificaion-area-icon-menu).

When creating a new shortcut, this name will be automatically generated until you manually edit the name, but after you edit it manually it will retain whatever value you provide.

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

## Adapter Busy Dialog
![Adapter Busy Dialog](./images/adapterbusy.png)

When an operation that mutates an adapter (recalling a shortcut, pasting an address, or renewing a DHCP lease) is in flight, a non-modal busy dialog appears for that adapter showing the current step. The dialog can be dismissed without aborting the operation, but closing it just hides the progress indicator while the operation continues in the background. If a second action is attempted on the same adapter while it is still busy, the dialog re-shows itself rather than starting a new operation.

Operations on different adapters run in parallel, and each gets its own busy dialog. The rest of the UI (selecting other adapters, viewing details, opening Settings, etc.) remains responsive throughout.

## Address Conflict Warning
![Address Conflict Warning](./images/addressconflictwarning.png)

After a refresh, if the application detects that two or more adapters are sharing the same IP address, a warning dialog is shown listing each conflict. Two adapters with the same address on the same machine is always a misconfiguration — Windows picks one and effectively ignores the other for routing purposes — so the warning is intended to surface accidents (e.g. recalling the same static-IP shortcut against two adapters).

The dialog has a "do not show again" checkbox; checking it suppresses future conflict warnings for the remainder of the session. Restart the application to re-enable warnings.

The application also performs a pre-apply check before assigning a static IP, refusing to create a conflict in the first place if it can detect one. The post-refresh warning catches conflicts that arise from outside the application or from operations that bypass the pre-apply check.

## Settings Window
This window allows changing the functionality of the program.

![Settings Window](./images/settings.png)

### Hide when minimized
Hides the window from the task bar when it is minimized (it can be shown again from the [Notification Area Icon Menu](#notificaion-area-icon-menu)).

### Start minimized
Automatically minimizes the application as soon as it launches. (If Hide when minimized is checked, it will also be hidden at launch.) The [Notification Area Icon](#notification-area-icon) is still available for shortcuts and to show the application. (The window may still appear briefly before minimizing; this is annoying but I haven't gotten around to fixing it yet.)

### Double clicking a shortcut will
Chooses what action to perform when a shortcut from the [Shortcuts List](#shortcuts-list) is double-clicked: edit the selected shortcut, or recall the selected shortcut.

### Control Panel file
Path to the network adapters control panel file. This should be able to be left to the default value (`ncpa.cpl`), but if for some reason that file is moved or not available on your system, any other file may be specified here and it will be launched when the [Control Panel](#control-panel) button is clicked.

### Hotkey
Pressing the key combination set here will show and bring the window to the front. (The default is `Ctrl + Alt + Shift + F11`)

### Start at log on
When this is checked, a task scheduler event is created to launch the program when you (you, not any other user) logs on to Windows. (Actually there's a 30 second delay because if it's too fast the taskbar icon doesn't get created and I can't be bothered to fix it.) The task is created to run with the highest privileges, so you won't get the UAC dialog if the program starts at log on.

### Resetting the Settings
If for some reason your settings get hosed (for example, if somehow it stores the window size to be microscopically small), you can hold down the `Shift` key while the program launches to reset all of the settings to their default values.


## Debug Messages Window
This window shows additional information about the actions the program is performing and the results of those actions. Note: this will delete all of your shortcuts too!

![Debug Messages Window](./images/debug.png)

### Tool Bar

![Debug Messages Window Tool Bar](./images/debugtoolbar.png)

#### Clear Log
Clears all entries from the debug messages log.

#### Copy Messages
Copies all of the selected debug messages to the clipboard.

#### Save Log
Saves the entire log to a file.

### Debug Messages
This list contains all of the debug messages. Clicking a line will select it, using `CTRL` and `Shift` will allow selecting multiple items.

`CTRL+C` will copy selected items to the clipboard. `CTRL+A` will select all items.

## Privilege Elevation & UAC Prompt

![UAC Prompt](./images/uac.png)

Privilege elevation is required to make changes to network configuration. The Settings app and `ncpa.cpl` route changes through the Network Connections service, which is already elevated and checks the caller's permissions itself — hence no UAC prompt. This application calls CIM/WMI directly, and those providers check the calling process's token, so the application has to be elevated up front. That's the UAC prompt at launch.

The software isn't digitally signed, so the UAC prompt shows "unknown publisher."

## Windows SmartScreen Warning
If you downloaded this file from the Internet, there's a good chance you'll see the Windows SmartScreen warning saying "Windows has protected your PC." This is because the application isn't digitally signed and known to be safe by Microsoft. The path to fixing this is too long and expensive for an in-house tool, so you'll just have to "trust me, bro."

## Things I Haven't Tested
If you find a bug, use the bug report feature. But there are some things that I know might not work well and I just can't be bothered to test for since this is a limited-audience tech tool.

* Different font DPI settings - may make text in forms cropped and unreadable
* Different window scaling settings - this should be OK but different elements on the form may not scale correctly or proportionately
* IPv6 - the software is generally aimed at IPv4 (and dotted-decimal addresses specifically), though it is possible to create IPv6 shortcuts

## AI Disclosure
This project was completed before AI coding agents were a thing, but recent revisions have used the aid of an agent for refactorings and documentation. (Release [1.0.5.1](https://github.com/SpectrumIntegrators/IPAddressChanger/releases/tag/v1.0.5.1) was the last non-AI-assisted release.)

## Copyright
	IP Address Changer - Windows GUI application to quickly change network address settings.
    Copyright (C) 2024-2026 Jonathan Dean (jonathand at spectrumintegrators.com)

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