using System.Linq;
using MentorWebApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace MentorWebApp.Data
{
    public static class ApplicationDbContextSeedData
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                //var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                ApplicationUser[] defaultUsers =
                {
                    //Admin
                    new ApplicationUser("admin", "Admin", "admin@mekb.com", "admin@mekb.com", "adminADMIN#123"),
                    new ApplicationUser("admin2", "Admin2", "admin2@mekb.com", "admin2@mekb.com", "adminADMIN#123"),

                    //Mentors
                    new ApplicationUser("daniel", "Mentor", "daniel@mekb.com", "daniel@mekb.com", "danielDANIEL#123"),
                    new ApplicationUser("andrew", "Mentor", "andrew@mekb.com", "andrew@mekb.com", "andrewANDREW#123"),
                    new ApplicationUser("panashe", "Mentor", "panashe@mekb.com", "panashe@mekb.com",
                        "panashePANASHE#123"),
                    new ApplicationUser("mentor1", "Mentor", "mentor1@mekb.com", "mentor1@mekb.com", "Mentor#123"),
                    new ApplicationUser("mentor2", "Mentor", "mentor2@mekb.com", "mentor2@mekb.com", "Mentor#123"),
                    new ApplicationUser("mentor3", "Mentor", "mentor3@mekb.com", "mentor3@mekb.com", "Mentor#123"),
                    new ApplicationUser("mentor4", "Mentor", "mentor4@mekb.com", "mentor4@mekb.com", "Mentor#123"),
                    new ApplicationUser("mentor5", "Mentor", "mentor5@mekb.com", "mentor5@mekb.com", "Mentor#123"),
                    new ApplicationUser("mentor6", "Mentor", "mentor6@mekb.com", "mentor6@mekb.com", "Mentor#123"),
                    new ApplicationUser("mentor7", "Mentor", "mentor7@mekb.com", "mentor7@mekb.com", "Mentor#123"),
                    new ApplicationUser("mentor8", "Mentor", "mentor8@mekb.com", "mentor8@mekb.com", "Mentor#123"),
                    new ApplicationUser("mentor9", "Mentor", "mentor9@mekb.com", "mentor9@mekb.com", "Mentor#123"),
                    new ApplicationUser("mentor10", "Mentor", "mentor10@mekb.com", "mentor10@mekb.com", "Mentor#123"),


                    //Mentees
                    new ApplicationUser("mentee1", "Mentee", "mentee1@mekb.com", "mentee1@mekb.com", "Mentee#123"),
                    new ApplicationUser("mentee2", "Mentee", "mentee2@mekb.com", "mentee2@mekb.com", "Mentee#123"),
                    new ApplicationUser("mentee3", "Mentee", "mentee3@mekb.com", "mentee3@mekb.com", "Mentee#123"),
                    new ApplicationUser("mentee4", "Mentee", "mentee4@mekb.com", "mentee4@mekb.com", "Mentee#123"),
                    new ApplicationUser("mentee5", "Mentee", "mentee5@mekb.com", "mentee5@mekb.com", "Mentee#123"),
                    new ApplicationUser("mentee6", "Mentee", "mentee6@mekb.com", "mentee6@mekb.com", "Mentee#123"),
                    new ApplicationUser("mentee7", "Mentee", "mentee7@mekb.com", "mentee7@mekb.com", "Mentee#123"),
                    new ApplicationUser("mentee8", "Mentee", "mentee8@mekb.com", "mentee8@mekb.com", "Mentee#123"),
                    new ApplicationUser("mentee9", "Mentee", "mentee9@mekb.com", "mentee9@mekb.com", "Mentee#123"),
                    new ApplicationUser("mentee10", "Mentee", "mentee10@mekb.com", "mentee10@mekb.com", "Mentee#123"),
                    new ApplicationUser("mentee11", "Mentee", "mentee11@mekb.com", "mentee11@mekb.com", "Mentee#123"),
                    new ApplicationUser("mentee12", "Mentee", "mentee12@mekb.com", "mentee12@mekb.com", "Mentee#123"),
                    new ApplicationUser("mentee13", "Mentee", "mentee13@mekb.com", "mentee13@mekb.com", "Mentee#123"),
                    new ApplicationUser("mentee14", "Mentee", "mentee14@mekb.com", "mentee14@mekb.com", "Mentee#123"),
                    new ApplicationUser("mentee15", "Mentee", "mentee15@mekb.com", "mentee15@mekb.com", "Mentee#123"),
                    new ApplicationUser("mentee16", "Mentee", "mentee16@mekb.com", "mentee16@mekb.com", "Mentee#123"),
                    new ApplicationUser("mentee17", "Mentee", "mentee17@mekb.com", "mentee17@mekb.com", "Mentee#123"),
                    new ApplicationUser("mentee18", "Mentee", "mentee18@mekb.com", "mentee18@mekb.com", "Mentee#123"),
                    new ApplicationUser("mentee19", "Mentee", "mentee19@mekb.com", "mentee19@mekb.com", "Mentee#123"),
                    new ApplicationUser("mentee20", "Mentee", "mentee20@mekb.com", "mentee20@mekb.com", "Mentee#123")
                };

                for (var i = 0; i < defaultUsers.Length; i++)
                    if (!context.Users.Any(u => u.UserName == defaultUsers[i].UserName))
                    {
                        userManager.CreateAsync(defaultUsers[i]).Wait();
                        var currrentRole = defaultUsers[i].Permissions;
                        switch (currrentRole)
                        {
                            case "Admin":
                                userManager.AddToRoleAsync(defaultUsers[i], "Admin").Wait();
                                break;
                            case "Mentor":
                                userManager.AddToRoleAsync(defaultUsers[i], "Mentor").Wait();
                                break;
                            case "Mentee":
                                userManager.AddToRoleAsync(defaultUsers[i], "Mentee").Wait();
                                break;
                        }
                    }

                //Resources

                Resource[] testResources =
                {
                    new Resource("Time management skills | University of Oxford",
                        "https://www.ox.ac.uk/students/academic/guidance/skills/time?wssl=1"),
                    new Resource("Essay Writing | University of Oxford",
                        "https://www.ox.ac.uk/students/academic/guidance/skills/time?wssl=1"),
                    new Resource("Essay writing in Science | University of Oxford",
                        "https://www.youtube.com/watch?v=gtt9sX4WTYY"),
                    new Resource("Developing a Mindset for Successful Learning | Samford University",
                        "https://youtu.be/htv6eap1-_M"),
                    new Resource(
                        "How to Get the Most Out of Studying: Part 1 of 5, 'Beliefs That Make You Fail...Or Succeed | Samford University'",
                        "https://youtu.be/RH95h36NChI"),
                    new Resource(
                        "How to Get the Most Out of Studying: Part 2 of 5, 'What Students Should Know About How People Learn' | Samford University",
                        "https://youtu.be/9O7y7XEC66M"),
                    new Resource(
                        "How to Get the Most Out of Studying: Part 3 of 5, 'Cognitive Principles for Optimizing Learning' | Samford University",
                        "https://youtu.be/1xeHh5DnCIw"),
                    new Resource(
                        "How to Get the Most Out of Studying: Part 4 of 5, 'Putting Principles for Learning into Practice' | Samford University",
                        "https://youtu.be/E9GrOxhYZdQ"),
                    new Resource(
                        "How to Get the Most Out of Studying: Part 5 of 5, 'I Blew the Exam, Now What ?' | Samford University",
                        "https://youtu.be/-QVRiMkdRsU"),
                    new Resource("Assessment and feedback: Understanding marking criteria | University of Reading",
                        "http://www.screencast.com/t/e7wnocHwx"),
                    new Resource("Assessment and feedback: Making the most of your feedback | University of Reading",
                        "http://www.screencast.com/t/ScrkqGUwk"),
                    new Resource("Essay writing: Overview of essay writing | University of Reading",
                        "http://www.screencast.com/t/2hybfnrDjL"),
                    new Resource("Essay writing: Answering the question and planning | University of Readin",
                        "http://www.screencast.com/t/uAXU1wWEX0"),
                    new Resource("Essay writing: Targeted Reading and Use of Evidence | University of Reading",
                        "http://www.screencast.com/t/T1v0QZeS"),
                    new Resource("Essay writing: Structuring your essay | University of Reading",
                        "http://www.screencast.com/t/o9SsJ5eyv"),
                    new Resource("Essay writing: Clear communication and proof reading | University of Reading",
                        "http://www.screencast.com/t/0zme7uKq"),
                    new Resource("What it means to be a critical student | University of Leicester",
                        "https://www.youtube.com/watch?v=YVLjziA5U2o"),
                    new Resource("Report writing: Finding a structure for your report | University of Reading ",
                        "https://youtu.be/YewU0bjh_Xw"),
                    new Resource("Reflective writing | University of Hull", "https://youtu.be/QoI67VeE3ds"),
                    new Resource(
                        "Dissertations and major projects: Starting your research for your dissertation | University of Reading",
                        "http://www.screencast.com/t/ULSvuvBN"),
                    new Resource(
                        "Dissertations and major projects: Defining a research question | University of Reading",
                        "http://www.screencast.com/t/sfJ1L62FOxpT"),
                    new Resource(
                        "Dissertations and major projects: Managing time for your dissertation | University of Reading",
                        "http://www.screencast.com/t/dDlLcYpHzv"),
                    new Resource(
                        "Dissertations and major projects: Structuring your dissertation | University of Reading",
                        "http://www.screencast.com/t/r0FBuz1hT0"),
                    new Resource("Dissertations and major projects: Doing a literature review | University of Reading",
                        "http://www.screencast.com/t/RdY4lwEW"),
                    new Resource(
                        "Dissertations and major projects: Writing up your dissertation | University of Reading",
                        "http://www.screencast.com/t/PbmRbnhETTHI"),
                    new Resource(
                        "Dissertations and major projects: Finishing off your dissertation | University of Reading",
                        "http://www.screencast.com/t/OUG2WySh"),
                    new Resource("Referencing: Avoiding Unintentional Plagiarism | University of Reading",
                        "http://www.screencast.com/t/cGOqGCglPF"),
                    new Resource("Referencing: How to use long and short quotes | University of Reading",
                        "http://www.screencast.com/t/98XGMa7udG7"),
                    new Resource("Referencing: Using paraphrases | University of Reading",
                        "http://www.screencast.com/t/44LKqUlyv"),
                    new Resource("Referencing: Writing a precis or summary | University of Reading",
                        "http://www.screencast.com/t/02khT1iGr"),
                    new Resource("Referencing: Which referencing style should I use? | University of Reading",
                        "http://www.screencast.com/t/IWJSVUtwAfG"),
                    new Resource("Referencing: Finding bibliographic details | University of Reading",
                        "http://www.screencast.com/t/P6TFKbuhSO"),
                    new Resource("Referencing: Referencing websites | University of Reading",
                        "http://www.screencast.com/t/3fkRYd752"),
                    new Resource("Referencing: Compiling your bibliography | University of Reading",
                        "http://www.screencast.com/t/NHjVee6k2n"),
                    new Resource(
                        "Referencing: Effective paraphrasing for postgraduate students | University of Reading",
                        "http://www.screencast.com/t/27J2IIlwVEZ"),
                    new Resource("Referencing: Getting the most out of Turnitin | University of Reading",
                        "http://www.screencast.com/t/3VKn4Mlx"),
                    new Resource("Researching your assignments: Reading Academic Texts | University of Reading",
                        "http://www.screencast.com/t/afiEROrgT"),
                    new Resource(
                        "Researching your assignments: Starting research for your assignment | University of Reading",
                        "http://www.screencast.com/t/XJEcEtCkCbR"),
                    new Resource("Researching your assignments: Critical notetaking | University of Reading",
                        "http://www.screencast.com/t/dsyfXpEB"),
                    new Resource("Researching your assignments: Evaluating your sources | University of Reading",
                        "http://www.screencast.com/t/SSFIzzl3Pq"),
                    new Resource(
                        "Researching your assignments: Introduction to your online reading list | University of Reading",
                        "http://www.screencast.com/t/DcNzRt1IND"),
                    new Resource("Preparing for exams: Effective revision | University of Reading",
                        "http://www.screencast.com/t/Ekgd6yGSl"),
                    new Resource("Preparing for exams: Exam room strategies | University of Reading",
                        "http://www.screencast.com/t/XLgvoL8yvH"),
                    new Resource("Preparing for exams: Preparing for essay exams | University of Reading",
                        "http://www.screencast.com/t/mJlsMHrbgh"),
                    new Resource("Preparing for exams: Answering Short Answer Questions | University of Reading",
                        "http://www.screencast.com/t/WzhUF4ljL1C"),
                    new Resource("Preparing for exams: Multiple Choice Question exams | University of Reading",
                        "http://www.screencast.com/t/rvOq5aup5a4"),
                    new Resource("Preparing for exams: Preparing for seen exams | University of Reading",
                        "http://www.screencast.com/t/DClw7TCf"),
                    new Resource("Preparing for exams: Preparing for open book exams | University of Reading",
                        "http://www.screencast.com/t/A9QgzTe7"),
                    new Resource("Preparing for exams: Preparing for oral language exams | University of Reading",
                        "http://www.screencast.com/t/uaV2M1jBhDtx"),
                    new Resource("Manage your time and get things done: Making a term plan | University of Reading",
                        "http://www.screencast.com/t/kGkPXL7rvjo"),
                    new Resource(
                        "Manage your time and get things done: Making a study timetable | University of Reading",
                        "http://www.screencast.com/t/sG1bpyQj2XeK"),
                    new Resource("Active Reading | York University", "https://www.youtube.com/watch?v=knfj03eHAZI"),
                    new Resource("Resources for Academic Success | York University",
                        "https://www.youtube.com/watch?v=oO1i1Qa4GLA"),
                    new Resource("Exam Preparation Part 1 | York University",
                        "https://www.youtube.com/watch?v=QFfZUtUwARw"),
                    new Resource("Exam Preparation Part 2 | York University",
                        "https://www.youtube.com/watch?v=QosgANTgrtw"),
                    new Resource("Exam Preparation (full) | York University",
                        "http://www.youtube.com/watch?v=Ny2ShnXWZxc"),
                    new Resource("Learning for Keeps | York University", "http://www.youtube.com/watch?v=mnID_kSPxHs"),
                    new Resource("Note-taking 1 | York University", "http://www.youtube.com/watch?v=Je9Aqffoshs"),
                    new Resource("Note-taking 2 | York University", "http://www.youtube.com/watch?v=ZBXksik0nHI"),
                    new Resource("Note-taking (full) | York University", "http://www.youtube.com/watch?v=0WMT-8cgZFA"),
                    new Resource("Stress Relief Exercise | York University",
                        "https://www.youtube.com/watch?v=mktff9ALL7g"),
                    new Resource("University Success Secrets | York University",
                        "http://www.youtube.com/watch?v=gxjGp5ENmG8"),
                    new Resource("A Student-led Workshop on Time Management | Cornell University",
                        "http://lsc.cornell.edu/study-skills/study-skills-videos/"),
                    new Resource("The Key to a Good Semester–The Big Picture | Cornell University",
                        "http://lsc.cornell.edu/study-skills/study-skills-videos/"),
                    new Resource("The Best Weekly Routine | Cornell University",
                        "http://lsc.cornell.edu/study-skills/study-skills-videos/"),
                    new Resource("The Key to Tests | Cornell University",
                        "http://lsc.cornell.edu/study-skills/study-skills-videos/"),
                    new Resource("The Key to Problem-Solving Tests | Cornell University",
                        "http://lsc.cornell.edu/study-skills/study-skills-videos/"),
                    new Resource("The Key to Good Notes | Cornell University",
                        "http://lsc.cornell.edu/study-skills/study-skills-videos/"),
                    new Resource("The Best Test Prep Structure | Cornell University",
                        "http://lsc.cornell.edu/study-skills/study-skills-videos/"),
                    new Resource("The Key to Multiple Choice Tests | Cornell University",
                        "http://lsc.cornell.edu/study-skills/study-skills-videos/"),
                    new Resource("The Key to Reading | Cornell University",
                        "http://lsc.cornell.edu/study-skills/study-skills-videos/"),
                    new Resource("How to Manage Your Time Better", "https://www.youtube.com/watch?v=VUk6LXRZMMk"),
                    new Resource("How to Stop Procrastinating", "https://www.youtube.com/watch?v=Qvcx7Y4caQE"),
                    new Resource("How To Study Smarter, Not Harder - From How We Learn by Benedict Carey",
                        "https://youtu.be/H-DJEU9N1y4"),
                    new Resource("How to Take Great Notes", "https://www.youtube.com/watch?v=UAhRf3U50lM"),
                    new Resource("Studying for Finals: Tips and Tricks", "https://youtu.be/jcSS9iCmacM"),
                    new Resource("Information Processing Theory | University of Southern California",
                        "https://youtu.be/J1TYC-I2vN0"),
                    new Resource("Learning, Memory, and Mindsets | University of Southern California",
                        "https://youtu.be/XzbhNh2Mv40"),
                    new Resource("Metacognition and Self-regulation | University of Southern California",
                        "https://youtu.be/PpWV1wpSH-g"),
                    new Resource("Social Cognitive Factors Part 1 | University of Southern California",
                        "https://www.youtube.com/watch?v=NVOFga9w-9E"),
                    new Resource("Social-Cognitive Factors Part 2 | University of Southern California",
                        "https://youtu.be/qpN44v7Mri0"),
                    new Resource("Equity and Access in Educational Contexts | University of Southern California",
                        "https://youtu.be/7eE4LbCAexU"),
                    new Resource("Cultural and Linguistic Diversity | University of Southern California",
                        "https://youtu.be/RxNlqZ12crY"),
                    new Resource("The Role of Emotion and Affect in Learning | University of Southern California",
                        "https://youtu.be/pDsKMNEJUvo"),
                    new Resource("Self-Regulation By Way of Goal Setting | University of Southern California",
                        "https://youtu.be/9ClXar53XcI"),
                    new Resource("How to Learn From Lectures and Textbooks | University of Southern California",
                        "https://youtu.be/auMS0jbc9Wc"),
                    new Resource("Preparing For and Taking Exams | University of Southern California",
                        "https://youtu.be/-1k4jX5MeX4"),
                    new Resource("Time Management Strategies | University of Southern California",
                        "https://youtu.be/y4HeNPrxRIU"),
                    new Resource("Strategies For Combating Procrastination  | University of Southern California",
                        "https://youtu.be/eocBjHb6v3U"),
                    new Resource("Regulation of Social and Physical Environment | University of Southern California",
                        "https://youtu.be/pSU7PLukwIU"),
                    new Resource("Concentration & Distraction | Oregon State University",
                        "https://youtu.be/hf2vyTs3lVo"),
                    new Resource("Procrastination, Motivation and Goal Setting | Oregon State University",
                        "https://youtu.be/O_QfTdYagO8"),
                    new Resource("Goal Setting | Oregon State University", "https://youtu.be/QcphRK2iZrY"),
                    new Resource("Mindset | Oregon State University", "https://youtu.be/o_e7xTNDCa4"),
                    new Resource("Student Responsibility | Oregon State University", "https://youtu.be/yPjFZxn5i08"),
                    new Resource("Homework and Practice | Oregon State University", "https://youtu.be/HMuik0FM1yU"),
                    new Resource("Reading | Oregon State University", "https://youtu.be/tjXM0KrWoeY"),
                    new Resource("Note-Taking | Oregon State University", "https://youtu.be/FNyrpOGtQ9k"),
                    new Resource("Test Anxiety | Oregon State University", "https://youtu.be/An9sHHspi7Q"),
                    new Resource("What is Academic Coaching? | Oregon State University",
                        "https://youtu.be/rPdTfC5UEjo"),
                    new Resource("The Memory Process | Oregon State University", "https://youtu.be/yuZAUJbjgLU"),
                    new Resource("Learning and Memory | Oregon State University", "https://youtu.be/0ijEMtA59lE")
                };

                for (var i = 0; i < testResources.Length; i++)
                    if (!context.Resources.Any(u => u.Link == testResources[i].Link))
                    {
                        var analytic = new ContentAnalytic(testResources[i].ResourceId);
                        testResources[i].Init(analytic);
                        context.Resources.AddAsync(testResources[i]).Wait();
                        context.ContentAnalytics.AddAsync(testResources[i].Analytic);
                        context.Resources.Add(testResources[i]);
                        context.ContentAnalytics.Add(testResources[i].Analytic);
                    }


                //Questions

                Question[] testQuestions =
                {
                    new Question("Where are the Libraries?", "UCT main library", "blgjoe001"),
                    new Question("When are the Libraries open?", "sdfsdf", "blgjoe001"),
                    new Question("What is plagiarism?", "sdfdfs", "blgjoe001"),
                    new Question("Where are the Scilabs?", "sdfsdfdsf", "blgjoe001"),
                    new Question("Where is the hotseat?", "sdfsdfsfdsfs", "blgjoe001"),
                    new Question("How do I query my marks?", "sdfdfsf", "blgjoe001"),
                    new Question("Where is the cafeteria?", "sdfsdfdf", "blgjoe001"),
                    new Question("How do I draw money?", "dsdffffdsdf", "blgjoe001"),
                    new Question("Where is Leslie Social?", "fdsdfdss", "blgjoe001"),
                    new Question("How do I change my tut period?", "sdfdsfs", "blgjoe001")
                };

                for (var i = 0; i < testQuestions.Length; i++)
                    if (!context.Questions.Any(u => u.Title == testQuestions[i].Title))
                    {
                        var analytic = new ContentAnalytic(testQuestions[i].Id);
                        testQuestions[i].Init(analytic);
                        context.Questions.AddAsync(testQuestions[i]).Wait();
                        context.ContentAnalytics.AddAsync(testQuestions[i].Analytic);
                        context.Questions.Add(testQuestions[i]);
                        context.ContentAnalytics.Add(testQuestions[i].Analytic);
                    }

                Reply[] testReplies =
                {
                    new Reply(testQuestions[0].Id, "reply...", "brlste069"),
                    new Reply(testQuestions[0].Id, "reply...", "brlste069"),
                    new Reply(testQuestions[3].Id, "reply...", "brlste069"),
                    new Reply(testQuestions[2].Id, "reply...", "brlste069"),
                    new Reply(testQuestions[3].Id, "reply...", "brlste069"),
                    new Reply(testQuestions[9].Id, "reply...", "brlste069"),
                    new Reply(testQuestions[5].Id, "reply...", "brlste069"),
                    new Reply(testQuestions[5].Id, "reply...", "brlste069"),
                    new Reply(testQuestions[2].Id, "reply...", "brlste069"),
                    new Reply(testQuestions[3].Id, "reply...", "brlste069"),
                    new Reply(testQuestions[0].Id, "reply...", "brlste069"),
                    new Reply(testQuestions[7].Id, "reply...", "brlste069"),
                    new Reply(testQuestions[3].Id, "reply...", "brlste069"),
                    new Reply(testQuestions[2].Id, "reply...", "brlste069"),
                    new Reply(testQuestions[3].Id, "reply...", "brlste069")
                };


                for (var i = 0; i < testReplies.Length; i++)
                    if (!context.Replies.Any(u => u.Id == testReplies[i].Id))
                    {
                        var analytic = new ContentAnalytic(testReplies[i].Id);
                        testReplies[i].Init(analytic);
                        context.Replies.Add(testReplies[i]);
                        context.ContentAnalytics.Add(testReplies[i].Analytic);
                    }

                //Finished

                context.SaveChanges();
                context.Dispose();
            }
        }
    }
}