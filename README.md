# DaBoM

Play time with **Byte Order Marker**

## What we are trying to achieve

It all started when I opened a CSV file for reading and my bytes coount was off but three bytes.  Turns out `EF` `BB` `BF` were the three errant bytes.  Strange, when I open the file with a `TextReader` the first `Read()` returned the forth character.  Obviously, there is magic afoot.  So, the goal is to account for the **Byte Order Marker**.

## How are we going to do this?

There are several items that expose the **BOM**, but first some background, in case you need more information on the **BOM** in general:

- [Wikipedia](https://simple.wikipedia.org/wiki/Byte_order_mark)
- [W3](https://www.w3.org/International/questions/qa-byte-order-mark)
- [unicode](https://unicode.org/faq/utf_bom.html)

1. First let's find some ways of actually 'seeing' the **BOM** in code.  I suspect there will be some stuff in the `System.Text` namespace so let's start there.  
1. The next place we'll go is some stream readers and writers, to see if we can capture a glimpse. 

> As a side note, we will just be playing in an xUnit project using _fluent assertions_.
