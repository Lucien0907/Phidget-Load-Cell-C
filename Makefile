BIN=VoltageRatioInputExample
SRCS=VoltageRatioInput_Example.c PhidgetHelperFunctions.c
LIBS=-lphidget22 -lncurses
CC=gcc
CFLAGS=-ggdb3 -Wall

all:
	${CC} ${CFLAGS} -o ${BIN} ${SRCS} ${LIBS}
