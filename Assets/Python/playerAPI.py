class OperationList:
    def __init__(self):
        self.text = []

    def append(self, ingredient: str):
        self.text.append(ingredient)

    def extend(self, ingredients: list):
        self.text += ingredients

    def get_self(self):
        return self

opList = OperationList()

def add_Flour():
    opList.append('Flour')

def add_Sugar():
    opList.append('Sugar')

def add_Milk():
    opList.append('Milk')

def add_Egg():
    opList.append('Egg')

def add_Frosting():
    opList.append('Frosting')

def add_Chocolate():
    opList.append('Chocolate')

def add_Sprinkles():
    opList.append('Sprinkles')

def add_Custom(ingredients: list):
    opList.extend(ingredients)