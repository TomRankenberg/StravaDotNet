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
mod <- aov(AverageHeartRate ~ ActiveRecentTimeAll+AverageSpeed+Distance+Type+BreakTime, data= strava2)
summary(mod)
mod$coefficients
plot(mod$coefficients[1] + 
     mod$coefficients[2]*strava2$ActiveRecentTimeAll+
       mod$coefficients[3]*strava2$AverageSpeed+
       mod$coefficients[4]*strava2$Distance,
     strava2$AverageHeartRate)

strava2$predictedHR <- mod$coefficients[1] + 
  mod$coefficients[2]*strava2$ActiveRecentTimeAll+
  mod$coefficients[3]*strava2$AverageSpeed+
  mod$coefficients[4]*strava2$Distance+
  mod$coefficients[7]*strava2$BreakTime

strava2$predictedHR <- ifelse(strava2$Type=="Run", strava2$predictedHR+mod$coefficients[5],
                              ifelse(strava2$Type=="Swim", strava2$predictedHR+mod$coefficients[6],
                              strava2$predictedHR))

ggplot(strava2, aes(x=predictedHR, y=AverageHeartRate))+
  geom_point()+geom_smooth(method='lm')+geom_abline(slope = 1)




