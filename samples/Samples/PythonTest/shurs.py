import numpy as np
from scipy import linalg
A = np.mat('[3 6 1; 23 13 1; 0 3 4]')
T, Z = linalg.schur(A)
print(T)
print(Z)