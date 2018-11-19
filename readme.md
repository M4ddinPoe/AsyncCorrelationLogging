# AsyncCorrelationLogging

This a demo to show how to add correlation id's to log entries so they can be traced through multiple concurrent threads or over the network.

The idea is based on this blogpost: https://blog.stephencleary.com/2013/04/implicit-async-context-asynclocal.html

The correlation id is passed with the `CallContext` along all called methods. 