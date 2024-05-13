const express = require('express');
const axios = require('axios');
const dotenv = require('dotenv');

dotenv.config();

const app = express();
app.use(express.json());

async function createQuestion(content, numberQuestion) {
  try {
    const response = await axios.post(
        'https://api.openai.com/v1/chat/completions',
        {
          messages: [
            {
              role: 'system',
              content: `You are helping a Vietnamese teacher generating ${numberQuestion} different questions, each question have multiple answers and at least 4, generate to JSON format, include randomly multiple and single correct answers, all the contents are in Vietnamese, the format is as follows:
                  title: brief title of the question;
                  content: content of the question in this format "<h2><strong>{{ content }}</strong></h2>";
                  metadata: {
                    answers: answers that students can choose between[
                      isCorrect: true/false,
                      textEditor: content of answer in this format  "<p>{{ content }}</p>"
                    ];
                  };
                }
              You can only answer with this Json format:  {questions: [....]}, you must return exactly ${numberQuestion} and can only return maximum 10 questions at a time even if I ask for more in the above. 
              `,
            },
            { role: 'user', content: content },
          ],
          model: 'gpt-3.5-turbo',
        },
        {
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${process.env.OPENAI_API_KEY}`,
          },
        }
    );

    return JSON.parse(response.data.choices[0].message.content);
  } catch (error) {
    throw new Error(error.message);
  }
}

app.post('/api/create-question', async (req, res) => {
  try {
    const { content, numberQuestion } = req.body;
    const questions = await createQuestion(content, numberQuestion);
    res.json(questions);
  } catch (error) {
    console.error(error);
    res.status(500).json({ error: 'An error occurred' });
  }
});

const port = process.env.PORT || 3000;
app.listen(port, () => {
  console.log(`Server is running on port ${port}`);
});