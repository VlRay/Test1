# Design & Methodology

We made separate class for the MarketLine and MarketCandle

We also add a class LogWatch to see the time elapse

Here a UML schema of our solution

![alt text](https://github.com/jzaoui26/fluenttechFinancial/blob/main/readme/Schema.png)

## Result & Screenshot

Here the result of the time (in the bottom of the screen)

Initialize 00:00:00.0238389 - GetMarketListFromCsv 00:00:00.0345928 - GetMarketCandle 00:00:00.0351395 - Datagrid 00:00:03.6311881 - Total 00:00:03.7247593

Screenshot of the result

![alt text](https://github.com/jzaoui26/fluenttechFinancial/blob/main/readme/Result.png)

## Low-latency, testability

We add a LogWatch to see the time elapse and do some tests

We also add 2 parameters in

- nameFileCsv = "MarketDataTest.csv"; // change the name of the file
- formatTimeLenght = 16; // 17/06/2022 12:23:17.308 - for the minutes the lenght has to be 16 - to have seconds you have to put 19 for example

## Improvement

We can maybe improve the ForEach from the list

![alt text](https://github.com/jzaoui26/fluenttechFinancial/blob/main/readme/Improvement.png)
