import numpy as np
import torch
import torch.nn as nn
import pandas as pd
from sentence_transformers import SentenceTransformer, util
import json
import sys

def top_5_similar_case(title, description, coeff=0.2):
    # define model
    model = SentenceTransformer('all-mpnet-base-v2')
    # load source data
    data = json.load(open("C:/Users/xukang/source/repos/MatchCaseService/MatchCaseService/Models/cases.json"))
    description_embedding = np.float32(np.array(list(data['description_embedding'].values())))
    title_embedding = np.float32(np.array(list(data['title_embedding'].values())))
    # encode target text and description
    target_title_embeddings = np.array(model.encode(title))
    title_cos_similarity = util.pytorch_cos_sim(target_title_embeddings, title_embedding).numpy()
    if description != '':
        target_description_embedding = model.encode(description)
        description_cos_similarity = util.pytorch_cos_sim(target_description_embedding, description_embedding).numpy()
        all_cos_similarity = coeff*title_cos_similarity + (1-coeff)*description_cos_similarity
    else:
        all_cos_similarity = title_cos_similarity

    # find the top 5 matching indexs
    all_cos_similarity = all_cos_similarity.squeeze()
    top_5_matching_idx = sorted(range(len(all_cos_similarity)), key=lambda i: all_cos_similarity[i])[-5:]
    top_5_matching_idx = top_5_matching_idx[::-1]
    top_5_matching_title = np.array(list(data['title'].values()))[top_5_matching_idx]
    top_5_matching_incidentid = np.array(list(data['incidentid'].values()))[top_5_matching_idx]
    result = {'titles': list(top_5_matching_title), 'ids':list(top_5_matching_idx)} 
    with open('C:/Users/xukang/source/repos/MatchCaseService/MatchCaseService/Controllers/res1.json', 'w') as f:
        json.dump(result,f)
    return result
    
        
if __name__ == '__main__':
    top_5_similar_case(sys.argv[1], sys.argv[2]) 
    