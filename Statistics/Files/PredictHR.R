setwd("C:/Users/TomRa/Documents/StravaTesting/")
library(dplyr)
library(lubridate)
library(ggplot2)

strava <- read.csv("activities2.csv")
colnames(strava) <- gsub("ActivityForStats.", "", colnames(strava))
colnames(strava)
head(strava)
strava$AverageHeartRate <- as.numeric(sub(",", ".", strava$AverageHeartRate, fixed = TRUE))
strava$Distance <- as.numeric(sub(",", ".", strava$Distance, fixed = TRUE))
strava$ElevationGain <- as.numeric(sub(",", ".", strava$ElevationGain, fixed = TRUE))
strava$AverageSpeed <- as.numeric(sub(",", ".", strava$AverageSpeed, fixed = TRUE))


head(strava)
strava$AverageSpeed = strava$Distance / strava$ElapsedTime

strava2 <- strava[strava$Type%in%c("Run", "Ride", "Swim") & strava$AverageHeartRate > 0,]
# Run
modRun <- aov(AverageHeartRate ~ ActiveRecentTimeThisType+AverageSpeed+Distance, data= strava2[strava2$Type=="Run",])
summary(modRun)
modRun$coefficients
plot(modRun$coefficients[1] + 
     modRun$coefficients[2]*strava2$ActiveRecentTimeThisType[strava2$Type == "Run"]+
       modRun$coefficients[3]*strava2$AverageSpeed[strava2$Type == "Run"]+
       modRun$coefficients[4]*strava2$Distance[strava2$Type == "Run"],
     strava2$AverageHeartRate[strava2$Type == "Run"])

# Ride
modRide <- aov(AverageHeartRate ~ ActiveRecentTimeThisType+AverageSpeed, data= strava2[strava2$Type=="Ride",])
summary(modRide)
modRide$coefficients
plot(modRide$coefficients[1] + 
       modRide$coefficients[2]*strava2$ActiveRecentTimeThisType[strava2$Type == "Ride"]+
       modRide$coefficients[3]*strava2$AverageSpeed[strava2$Type == "Ride"],
     strava2$AverageHeartRate[strava2$Type == "Ride"])

# Swim
modSwim <- aov(AverageHeartRate ~ ActiveRecentTimeThisType, data= strava2[strava2$Type=="Swim",])
summary(modSwim)
modSwim$coefficients
plot(modSwim$coefficients[1] + 
       modSwim$coefficients[2]*strava2$ActiveRecentTimeThisType[strava2$Type == "Swim"],
     strava2$AverageHeartRate[strava2$Type == "Swim"])



