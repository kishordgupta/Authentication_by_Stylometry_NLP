# Authentication_by_Stylometry_NLP
This Projects aim to evaluate the efficiency of Stylometry approach as an authentication method. Stylometric analysis research has been done for author identification and there is significant progress to recognize an author based on their written texts. In this project, we tried to detect differences between writing styles on the same topic provided by a set of users and we test that these differences are enough to use for an authentication system or not.

# Introduction
Authentication is a protocol which maintains that only a valid user will be able to get access to a
digital asset. Fallback authentication is a support protocol system when users forgot verification
credential and need to reset the credentials. Currently, the most popular kind of fallback authentication is some set of security questions. If users able to answer these questions, the system will
accept the user as a valid user and reset their credentials.
Security questions are not secure anymore as the current age of social network progress, personal
information which used to form reliable questions are now open in public eye, Hackers can easily
guess the answer of these questions and can get past the authentication system. We want to apply
the stylometric approach to employ a fallback authentication system. Stylometry is the study of
writing style. Author Ramaya et el [4] says “There is an unconscious aspect to an author’s style that
cannot be consciously manipulated but which sesses quantifiable and distinctive features.” Based
on that we can say with precise feature identification it should be sible to identify the author. But
in authentication system, writing sample won’t is enough due to usability reason. So it will be a
challenge to use it for authentication pure. In this project, we tried to detect feature is small size
text sample and later used that for authentication pure. We evaluate our result, and we find that
stylometry can be useful with the help of other authentication factors.

# Experiments & Results
For my experiment, I used the Amazon product review data set, where the same users provided a
review on similar products. We randomly took 63 reviewers, all of them have more than 7 product
review. And all products are office suppliers tools. This way we can make each text domain
specific. No analysis is more than two lines.
I first remove 1 of the review as a test sample from each of the users and start our process on
remaining reviews. We extracted 45 feature. Thirty-six of them are the ratio of POS tag usage.
Others are some lexical features. I used c-sharp and a wrapper of Stanford NLP library to generate
feature data. We take 45 feature as shown in figure 3. After I made the feature data using the
math function, I calculate the average and standard deviations of feature value. With that, I
created a profile for each user using c-sharp services and saved them in a CSV file. During login
time, I generated profile value in the same way and checked the match percentage with the userid
associated with the sample text. Based on that percentage login successful and failure has decided.
n
In figure 4,the extracted feature data has showed. From all of these feature data, i generated
upper bound and lower bound of feature which will use for as showed in figure 5. Based on
the bound data, we generated profile information of each users as we described in our approach.
Sample profile data has showed in figure 6. Above way I developed a database with userid and
their profiles id, For testing purpose, I developed a c-sharp winform application as showed in figure
7.
In our experiment, I found a significant percentage of time writing style can be matched with
profile values above 50%. But with cross-testing, I saw it don’t have the performance accuracy I
need for an authentication purpose.
But the users who have larger text sample has more accuracy. And the deviation of profile
values. So it may be possible that if we add more features. This approach can be used in an
authentication system.
# Conclusions and future work
From my experiment, it is evident that the current approach has not enough unique features to
correctly identify authors. But combining with more strategy and adding new features it may
be possible to increase the efficiency. In the current state, it is not reliable method to use in
authentication system. In my future work, I want to add other approach combining with the
correct and try to improve the system.
