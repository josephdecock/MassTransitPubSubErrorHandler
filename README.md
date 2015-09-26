# MassTransitPubSubErrorHandler

Example code related to a [StackOverflow question I asked](http://stackoverflow.com/questions/32461208/masstransit-running-code-when-messages-sent-to-error-queue).

What I'm trying to show is a publisher and subscriber where the subscriber might experience transient errors. The subscriber is using a retry policy (for demo purposes, it doesn't try very hard) that eventually gives up if it can't perform its work. If that happens, I'd like to take some sort of action to notify the outside world of this failure - maybe send an email to the sysadmins, maybe publish a message to indicate that failure has occurred - but in general, I think it would be useful to be able to have a hook that runs when a message goes to the error queue. 
