# https://docs.python.org/3/tutorial/controlflow.html#if-statements

# For this level, we will create our own function.
# We start with the 'def' keyword. 'def' lets Python
# know we want to define something.
# Type the following:
# def build_Cake(sprinkles):
#     if sprinkles == True:
#         return ["Egg", "Milk", "Flour", "Sugar", "Frosting", "Sprinkles"]
#     else:
#         return ["Egg", "Milk", "Flour", "Sugar", "Frosting"]

# 'build_Cake' is the name of our function. It accepts an argument,
# which we call 'sprinkles'. sprinkles can be True or False.
# If it is True, that means we want sprinkles on our cake.
# 'return' is where the function ends, and it specifies a value
# to put back wherever the function is called.
# For example:
cake_with_sprinkles = build_Cake(True)
# The value of cake_with_sprinkles is now:
# ["Egg", "Milk", "Flour", "Sugar", "Frosting", "Sprinkles"]

# Now type:
# for i in range(5):
#     add_Custom(build_Cake(False))
#     add_Custom(build_Cake(True))

# We can put function calls inside of function calls to immediately
# use their return values.
# This will create 5 cakes without sprinkles and 5 cakes with sprinkles.