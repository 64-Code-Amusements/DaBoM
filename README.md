# DaBoM

Play time with **Byte Order Marker**

## What we are trying to achieve

It all started when I opened a CSV file for reading and my bytes coount was off but three bytes.  Turns out `EF` `BB` `BF` were the three errant bytes.  Strange, when I open the file with a `TextReader` the first `Read()` returned the forth character.  Obviously, there is magic afoot.  So, the goal is to account for the **Byte Order Marker**.

## How are we going to do this?
