import torch
from transformers import GPT2LMHeadModel, GPT2Tokenizer

# Load pre-trained model and tokenizer
model_name = "gpt2"  # Can be 'gpt2-medium' or another if you want better responses
model = GPT2LMHeadModel.from_pretrained(model_name)
tokenizer = GPT2Tokenizer.from_pretrained(model_name)

# Set pad token if it doesn't exist (GPT-2 has no official pad token)
tokenizer.pad_token = tokenizer.eos_token
model.config.pad_token_id = model.config.eos_token_id

# Make sure model is in eval mode
model.eval()

# Generate a response
def generate_response(user_input):
    # Q&A format helps guide the model
    prompt = f"Q: {user_input}\nA:"

    # Encode with attention mask
    inputs = tokenizer.encode(prompt, return_tensors="pt")
    attention_mask = torch.ones_like(inputs)

    # Generate response
    with torch.no_grad():
        outputs = model.generate(
            inputs,
            attention_mask=attention_mask,
            max_length=200,
            pad_token_id=tokenizer.pad_token_id,
            no_repeat_ngram_size=2,
            do_sample=True,
            temperature=0.7,
            top_k=50,
            top_p=0.95,
            num_return_sequences=1
        )

    # Decode and clean response
    full_output = tokenizer.decode(outputs[0], skip_special_tokens=True)
    response = full_output[len(prompt):].strip()  # Remove the prompt from the response
    return response

# CLI chat loop
def chat():
    print("Welcome to ChatGPT-like CLI chatbot! Type 'quit' to exit.")
    while True:
        user_input = input("You: ")
        if user_input.lower() == 'quit':
            print("Goodbye!")
            break
        else:
            response = generate_response(user_input)
            print(f"ChatBot: {response}")

if __name__ == "__main__":
    chat()
